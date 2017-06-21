using JDRE.JVM.runtime;
using JDRE.JVM.runtime.Heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.instructions.Base
{

    interface Instruction
    {
        void FetchOperands(BytecodeReader reader);
        void Execute(Frame frame);
    }

    class NoOperandsInstruction : Instruction
    {
        public virtual void Execute(Frame frame)
        {

        }

        public virtual void FetchOperands(BytecodeReader reader)
        {

        }
    };

    class BranchInstruction : Instruction
    {
        public int Offset { get; private set; }

        public static void BranchJump(Frame frame, int offset)
        {
            frame.NextPC = frame.Thread.PC + offset;
        }

        public virtual void Execute(Frame frame)
        {

        }

        public void FetchOperands(BytecodeReader reader)
        {
            Offset = reader.ReadInt16();
        }
    }

    class Index8Instruction : Instruction
    {
        public int Index { get; set; }

        public virtual void Execute(Frame frame)
        {

        }

        public void FetchOperands(BytecodeReader reader)
        {
            Index = (int)reader.ReadUInt8();
        }
    }

    class Index16Instruction : Instruction
    {
        public int Index { get; private set; }

        public virtual void Execute(Frame frame)
        {

        }

        public void FetchOperands(BytecodeReader reader)
        {
            Index = reader.ReadUInt16();
        }
    }

    class RETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.Thread.PopFrame();
        }
    }
    class IRETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Thread t = frame.Thread;
            Frame curFrame = t.PopFrame();
            Frame invoker = t.CurrentFrame();
            invoker.OperandStack.PushInt32(curFrame.OperandStack.PopInt32());
        }
    }
    class LRETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Thread t = frame.Thread;
            Frame curFrame = t.PopFrame();
            Frame invoker = t.CurrentFrame();
            invoker.OperandStack.PushLong(curFrame.OperandStack.PopLong());
        }
    }
    class FRETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Thread t = frame.Thread;
            Frame curFrame = t.PopFrame();
            Frame invoker = t.CurrentFrame();
            invoker.OperandStack.PushFloat(curFrame.OperandStack.PopFloat());
        }
    }
    class DRETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Thread t = frame.Thread;
            Frame curFrame = t.PopFrame();
            Frame invoker = t.CurrentFrame();
            invoker.OperandStack.PushDouble(curFrame.OperandStack.PopDouble());
        }
    }
    class ARETURN : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Thread t = frame.Thread;
            Frame curFrame = t.PopFrame();
            Frame invoker = t.CurrentFrame();
            invoker.OperandStack.PushObject(curFrame.OperandStack.PopObject());
        }
    }

    static class InvokerHelper
    {
        public static void InvokeMethod(Frame frame, Method method)
        {
            Thread thread = frame.Thread;
            Frame newFrame = thread.NewFrame(method);
            thread.PushFrame(newFrame);

            int argSlots = method.ArgSlotCount;
            for (int i = argSlots - 1; i >= 0; i--)
            {
                Slot slot = frame.OperandStack.Pop();
                frame.LocalVariables.SetSlot(i, slot);
            }
        }

        public static void println(OperandStack stack, string descriptor)
        {
            switch (descriptor)
            {
                case "(Z)V": Console.WriteLine(stack.PopInt32() != 0); break;
                case "(C)V": Console.WriteLine(stack.PopInt32()); break;
                case "(B)V": Console.WriteLine(stack.PopInt32()); break;
                case "(S)V": Console.WriteLine(stack.PopInt32()); break;
                case "(I)V": Console.WriteLine(stack.PopInt32()); break;
                case "(J)V": Console.WriteLine(stack.PopLong()); break;
                case "(F)V": Console.WriteLine(stack.PopFloat()); break;
                case "(D)V": Console.WriteLine(stack.PopDouble()); break;
                default:
                    Console.WriteLine("OUT: " + descriptor);
                    break;
            }
            stack.PopObject();
        }
    }
}
