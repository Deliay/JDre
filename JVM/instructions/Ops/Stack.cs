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
            Solt solt = frame.OperandStack.Pop();
            frame.OperandStack.Push(solt);
            frame.OperandStack.Push(solt);
        }
    }
    class DUP_X1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Solt solt1 = frame.OperandStack.Pop();
            Solt solt2 = frame.OperandStack.Pop();
            frame.OperandStack.Push(solt1);
            frame.OperandStack.Push(solt2);
            frame.OperandStack.Push(solt1);
        }
    }
    class DUP_X2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Solt solt1 = frame.OperandStack.Pop();
            Solt solt2 = frame.OperandStack.Pop();
            Solt solt3 = frame.OperandStack.Pop();
            frame.OperandStack.Push(solt1);
            frame.OperandStack.Push(solt3);
            frame.OperandStack.Push(solt2);
            frame.OperandStack.Push(solt1);
        }
    }

    class DUP2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Solt solt1 = frame.OperandStack.Pop();
            Solt solt2 = frame.OperandStack.Pop();
            frame.OperandStack.Push(solt2);
            frame.OperandStack.Push(solt1);
            frame.OperandStack.Push(solt2);
            frame.OperandStack.Push(solt1);
        }
    }
    class DUP2_X1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Solt solt1 = frame.OperandStack.Pop();
            Solt solt2 = frame.OperandStack.Pop();
            Solt solt3 = frame.OperandStack.Pop();
            frame.OperandStack.Push(solt2);
            frame.OperandStack.Push(solt1);
            frame.OperandStack.Push(solt3);
            frame.OperandStack.Push(solt2);
            frame.OperandStack.Push(solt1);
        }
    }
    class DUP2_X2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Solt solt1 = frame.OperandStack.Pop();
            Solt solt2 = frame.OperandStack.Pop();
            Solt solt3 = frame.OperandStack.Pop();
            Solt solt4 = frame.OperandStack.Pop();
            frame.OperandStack.Push(solt2);
            frame.OperandStack.Push(solt1);
            frame.OperandStack.Push(solt4);
            frame.OperandStack.Push(solt3);
            frame.OperandStack.Push(solt2);
            frame.OperandStack.Push(solt1);
        }
    }

    class SWAP : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            Solt solt1 = frame.OperandStack.Pop();
            Solt solt2 = frame.OperandStack.Pop();
            frame.OperandStack.Push(solt1);
            frame.OperandStack.Push(solt2);
        }
    }
}
