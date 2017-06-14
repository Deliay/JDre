using JDRE.JVM.instructions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDRE.JVM.runtime;

namespace JDRE.JVM.instructions.Constants
{
    class NOP : NoOperandsInstruction { };
    class ACONST_NULL : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushObject(null);
        }
    }
    class DCONST_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushDouble(0.0d);
        }
    }
    class DCONST_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushDouble(1.0d);
        }
    }
    class FCONST_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushFloat(0.0f);
        }
    }
    class FCONST_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushFloat(1.0f);
        }
    }
    class FCONST_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushFloat(2.0f);
        }
    }
    class ICONST_M1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushInt32(-1);
        }
    }
    class ICONST_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushInt32(0);
        }
    }
    class ICONST_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushInt32(1);
        }
    }
    class ICONST_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushInt32(2);
        }
    }
    class ICONST_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushInt32(3);
        }
    }
    class ICONST_4 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushInt32(4);
        }
    }
    class ICONST_5 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushInt32(5);
        }
    }
    class LCONST_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushLong(0);
        }
    }
    class LCONST_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            frame.OperandStack.PushLong(1);
        }
    }
    class BIPUSH : Instruction
    {
        public byte value;

        public void Execute(Frame frame)
        {
            int v = value;
            frame.OperandStack.PushInt32(v);
        }

        public void FetchOperands(BytecodeReader reader)
        {
            value = reader.ReadUInt8();
        }
    }
    class SIPUSH : Instruction
    {
        public Int16 value;

        public void Execute(Frame frame)
        {
            int v = value;
            frame.OperandStack.PushInt32(v);
        }

        public void FetchOperands(BytecodeReader reader)
        {
            value = reader.ReadInt16();
        }
    }
}
