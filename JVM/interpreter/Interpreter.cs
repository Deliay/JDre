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
        public Interpreter(MemberInfo methodInfo)
        {
            CodeAttribute codeAttr = methodInfo.CodeAttr();
            int maxLocal = codeAttr.maxLocals;
            int maxStack = codeAttr.maxStack;
            byte[] bytecode = codeAttr.code;

            Thread thread = Thread.CreateThread();
            Frame frame = thread.NewFrame(maxLocal, maxStack);
            thread.PushFrame(frame);

            try
            {
                Loop(thread, bytecode);
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
                //Console.WriteLine("PC:{0}, INST:{1} {2}", pc, inst.GetType().Name, inst);
#endif
                inst.Execute(frame);
            }
        }
    }
}
