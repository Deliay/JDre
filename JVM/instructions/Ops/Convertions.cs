using JDRE.JVM.instructions.Base;
using JDRE.JVM.runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.instructions.Convertions
{
    //基本数值类型转换
    //I2 LDF
    //L2 DFI BCS
    //D2 FIL
    //F2 LDI

    class I2L : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int d = frame.OperandStack.PopInt32();
            frame.OperandStack.PushLong((long)d);
        }
    }
    class I2D : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int d = frame.OperandStack.PopInt32();
            frame.OperandStack.PushDouble((double)d);
        }
    }
    class I2F : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int d = frame.OperandStack.PopInt32();
            frame.OperandStack.PushFloat((float)d);
        }
    }
    class I2B : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int d = frame.OperandStack.PopInt32();
            frame.OperandStack.PushFloat((byte)d);
        }
    }
    class I2C : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int d = frame.OperandStack.PopInt32();
            frame.OperandStack.PushFloat((char)d);
        }
    }
    class I2S : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int d = frame.OperandStack.PopInt32();
            frame.OperandStack.PushFloat((short)d);
        }
    }

    class L2I : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long d = frame.OperandStack.PopLong();
            frame.OperandStack.PushInt32((Int32)d);
        }
    }
    class L2D : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long d = frame.OperandStack.PopLong();
            frame.OperandStack.PushDouble((double)d);
        }
    }
    class L2F : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long d = frame.OperandStack.PopLong();
            frame.OperandStack.PushFloat((float)d);
        }
    }

    class D2L : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            double d = frame.OperandStack.PopDouble();
            frame.OperandStack.PushLong((long)d);
        }
    }
    class D2I : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            double d = frame.OperandStack.PopDouble();
            frame.OperandStack.PushInt32((int)d);
        }
    }
    class D2F : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            double d = frame.OperandStack.PopDouble();
            frame.OperandStack.PushFloat((float)d);
        }
    }

    class F2L : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            float d = frame.OperandStack.PopFloat();
            frame.OperandStack.PushLong((long)d);
        }
    }
    class F2I : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            float d = frame.OperandStack.PopFloat();
            frame.OperandStack.PushInt32((int)d);
        }
    }
    class F2D : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            float d = frame.OperandStack.PopFloat();
            frame.OperandStack.PushDouble((double)d);
        }
    }

}
