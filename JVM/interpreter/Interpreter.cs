using JDRE.JVM.classfile;
using JDRE.JVM.instructions;
using JDRE.JVM.instructions.Base;
using JDRE.JVM.instructions.Initial;
using JDRE.JVM.runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.interpreter
{
    class Interpreter
    {
        public Interpreter(runtime.Heap.Method method)
        {
            //CodeAttribute codeAttr = methodInfo.CodeAttr();
            int maxLocal = method.MaxLocals;
            int maxStack = method.MaxStack;

            Thread thread = Thread.CreateThread();
            Frame frame = thread.NewFrame(method);
            thread.PushFrame(frame);

            try
            {
                Loop(thread, method.Code);
            }
            catch //(Exception e)
            {
                Console.WriteLine("==========DONE===========");
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
            }
        }

        public void Loop(Thread thread, byte[] code)
        {
            Frame frame = thread.PopFrame();
            BytecodeReader reader = new BytecodeReader();
            while(true)
            {
                int pc = frame.NextPC;
                thread.PC = pc;

                reader.Reset(code, pc);

                byte opcode = reader.ReadUInt8();
                Instruction inst = InstructionAllocatorHelper.CreateInstruction(opcode);
                inst.FetchOperands(reader);
                frame.NextPC = reader.PC;

#if (DEBUG)
                Console.WriteLine("OK\t{0}\t{1}", pc, inst.GetType().Name);
#endif
                inst.Execute(frame);
            }
        }
    }
}
