using JDRE.JVM.classfile;
using JDRE.JVM.instructions;
using JDRE.JVM.instructions.Base;
using JDRE.JVM.instructions.Initial;
using JDRE.JVM.runtime;
using JDRE.JVM.runtime.Heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.interpreter
{
    class Interpreter
    {
        public Interpreter(Method method, bool logInst)
        {
            //CodeAttribute codeAttr = methodInfo.CodeAttr();
            int maxLocal = method.MaxLocals;
            int maxStack = method.MaxStack;

            Thread thread = Thread.CreateThread();
            Frame frame = thread.NewFrame(method);
            thread.PushFrame(frame);

            try
            {
                Loop(thread, logInst);
            }
            catch //(Exception e)
            {
                logFrames(thread);
            }
        }

        private void logFrames(Thread thread)
        {
            if (thread.IsStackEmpty()) return;
            foreach (var frame in thread.Frames)
            {
                Console.WriteLine("=====LOCAL VARIABLES=====");
                foreach (var item in frame.LocalVariables)
                {
                    Console.WriteLine(item.Number);
                }
                Console.WriteLine("====LOCAL STACKES=====");
                foreach (var item in frame.OperandStack)
                {
                    Console.WriteLine(item.Number);
                }

                Console.WriteLine(string.Format(">> PC:{0}, {1}.{2}", frame.NextPC, frame.Method.Name, frame.Method.Descriptor));
            }

        }

        public void Loop(Thread thread,bool logInst)
        {
            BytecodeReader reader = new BytecodeReader();
            while(true)
            {
                Frame frame = thread.CurrentFrame();
                int pc = frame.NextPC;
                thread.PC = pc;

                reader.Reset(frame.Method.Code, pc);

                byte opcode = reader.ReadUInt8();
                Instruction inst = InstructionAllocatorHelper.CreateInstruction(opcode);
                inst.FetchOperands(reader);
                frame.NextPC = reader.PC;

                if (logInst) logInstruction(frame, inst);

                inst.Execute(frame);

                if (thread.IsStackEmpty()) break;
            }
        }

        private void logInstruction(Frame frame, Instruction inst)
        {
            Console.WriteLine("{0}.{1}() #{2} {3}", frame.Method.Clazz.Name, frame.Method.Name, frame.Thread.PC, inst.GetType().Name);
        }
    }
}
