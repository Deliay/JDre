using JDRE.JVM.instructions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDRE.JVM.runtime;

namespace JDRE.JVM.instructions.Stack
{ 
    class POP : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.Pop();
        }
    }
    class POP2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.Pop();
        }
    }

    class DUP : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Slot slot = frame.OperandStack.Pop();
            frame.OperandStack.Push(slot);
            frame.OperandStack.Push(slot);
        }
    }
    class DUP_X1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Slot slot1 = frame.OperandStack.Pop();
            Slot slot2 = frame.OperandStack.Pop();
            frame.OperandStack.Push(slot1);
            frame.OperandStack.Push(slot2);
            frame.OperandStack.Push(slot1);
        }
    }
    class DUP_X2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Slot slot1 = frame.OperandStack.Pop();
            Slot slot2 = frame.OperandStack.Pop();
            Slot slot3 = frame.OperandStack.Pop();
            frame.OperandStack.Push(slot1);
            frame.OperandStack.Push(slot3);
            frame.OperandStack.Push(slot2);
            frame.OperandStack.Push(slot1);
        }
    }

    class DUP2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Slot slot1 = frame.OperandStack.Pop();
            Slot slot2 = frame.OperandStack.Pop();
            frame.OperandStack.Push(slot2);
            frame.OperandStack.Push(slot1);
            frame.OperandStack.Push(slot2);
            frame.OperandStack.Push(slot1);
        }
    }
    class DUP2_X1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Slot slot1 = frame.OperandStack.Pop();
            Slot slot2 = frame.OperandStack.Pop();
            Slot slot3 = frame.OperandStack.Pop();
            frame.OperandStack.Push(slot2);
            frame.OperandStack.Push(slot1);
            frame.OperandStack.Push(slot3);
            frame.OperandStack.Push(slot2);
            frame.OperandStack.Push(slot1);
        }
    }
    class DUP2_X2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Slot slot1 = frame.OperandStack.Pop();
            Slot slot2 = frame.OperandStack.Pop();
            Slot slot3 = frame.OperandStack.Pop();
            Slot slot4 = frame.OperandStack.Pop();
            frame.OperandStack.Push(slot2);
            frame.OperandStack.Push(slot1);
            frame.OperandStack.Push(slot4);
            frame.OperandStack.Push(slot3);
            frame.OperandStack.Push(slot2);
            frame.OperandStack.Push(slot1);
        }
    }

    class SWAP : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Slot slot1 = frame.OperandStack.Pop();
            Slot slot2 = frame.OperandStack.Pop();
            frame.OperandStack.Push(slot1);
            frame.OperandStack.Push(slot2);
        }
    }
}
