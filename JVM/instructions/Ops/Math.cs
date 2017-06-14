using JDRE.JVM.instructions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDRE.JVM.runtime;

namespace JDRE.JVM.instructions.Math
{ 
    //rem series op
    class IREM : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v1 = frame.OperandStack.PopInt32();
            int v2 = frame.OperandStack.PopInt32();
            if(v2 == 0)
            {
                throw new System.ArithmeticException();
            }
            int result = v1 % v2;
            frame.OperandStack.PushInt32(result);
        }
    }
    class LREM : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long v1 = frame.OperandStack.PopLong();
            long v2 = frame.OperandStack.PopLong();
            if (v2 == 0)
            {
                throw new System.ArithmeticException();
            }
            long result = v1 % v2;
            frame.OperandStack.PushLong(result);
        }
    }
    class DREM : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            double v1 = frame.OperandStack.PopDouble();
            double v2 = frame.OperandStack.PopDouble();

            double result = System.Math.IEEERemainder(v1, v2);

            frame.OperandStack.PushDouble(result);
        }
    }
    class FREM : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            float v1 = frame.OperandStack.PopFloat();
            float v2 = frame.OperandStack.PopFloat();

            float result = (float)System.Math.IEEERemainder(v1, v2);

            frame.OperandStack.PushFloat(result);
        }
    }

    static class SHRHelper
    {
        public static int USHR(this int val, int pos)
        {
            int result = val;
            if(pos != 0)
            {
                int mask = 0x7fffffff;
                result >>= 1;
                result &= mask;
                result >>= pos - 1;
            }
            return result;
        }
        public static long USHR(this long val, int pos)
        {
            long result = val;
            if (pos != 0)
            {
                int mask = 0x7fffffff;
                result >>= 1;
                result &= mask;
                result >>= pos - 1;
            }
            return result;
        }
    }
    //int/long 左移/右移/无符号右移
    class ISHL : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();
            int result = v1 << (v2 & 0x1f);
            frame.OperandStack.PushInt32(result);
        }
    }
    class ISHR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();
            int result = v1 >> (v2 & 0x1f);
            frame.OperandStack.PushInt32(result);
        }
    }
    class IUSHR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();
            frame.OperandStack.PushInt32(v1.USHR((v2 & 0x1f)));
        }
    }
    class LSHL : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            long v1 = frame.OperandStack.PopInt32();
            long result = v1 << (v2 & 0x3f);
            frame.OperandStack.PushLong(result);
        }
    }
    class LSHR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            long v1 = frame.OperandStack.PopInt32();
            long result = v1 >> (v2 & 0x3f);
            frame.OperandStack.PushLong(result);
        }
    }
    class LUSHR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            long v1 = frame.OperandStack.PopInt32();
            frame.OperandStack.PushLong(v1.USHR((v2 & 0x3f)));
        }
    }

    //按位 I/L and/or/xor
    class IAND : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();

            frame.OperandStack.PushInt32(v2 & v1);
        }
    }
    class IOR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();

            frame.OperandStack.PushInt32(v2 | v1);
        }
    }
    class IXOR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();

            frame.OperandStack.PushInt32(v2 ^ v1);
        }
    }

    class LAND : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long v2 = frame.OperandStack.PopLong();
            long v1 = frame.OperandStack.PopLong();

            frame.OperandStack.PushLong(v2 & v1);
        }
    }
    class LOR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long v2 = frame.OperandStack.PopLong();
            long v1 = frame.OperandStack.PopLong();

            frame.OperandStack.PushLong(v2 | v1);
        }
    }
    class LXOR : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long v2 = frame.OperandStack.PopLong();
            long v1 = frame.OperandStack.PopLong();

            frame.OperandStack.PushLong(v2 ^ v1);
        }
    }

    //加法
    class IADD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v1 = frame.OperandStack.PopInt32();
            int v2 = frame.OperandStack.PopInt32();
            frame.OperandStack.PushInt32(v1 + v2);
        }
    }
    class LADD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long v1 = frame.OperandStack.PopLong();
            long v2 = frame.OperandStack.PopLong();
            frame.OperandStack.PushLong(v1 + v2);
        }
    }
    class FADD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            float v1 = frame.OperandStack.PopFloat();
            float v2 = frame.OperandStack.PopFloat();
            frame.OperandStack.PushFloat(v1 + v2);
        }
    }
    class DADD : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            double v1 = frame.OperandStack.PopDouble();
            double v2 = frame.OperandStack.PopDouble();
            frame.OperandStack.PushDouble(v1 + v2);
        }
    }

    //减法
    class ISUB : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v1 = frame.OperandStack.PopInt32();
            int v2 = frame.OperandStack.PopInt32();
            frame.OperandStack.PushInt32(v1 - v2);
        }
    }
    class LSUB : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long v1 = frame.OperandStack.PopLong();
            long v2 = frame.OperandStack.PopLong();
            frame.OperandStack.PushLong(v1 - v2);
        }
    }
    class FSUB : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            float v1 = frame.OperandStack.PopFloat();
            float v2 = frame.OperandStack.PopFloat();
            frame.OperandStack.PushFloat(v1 - v2);
        }
    }
    class DSUB : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            double v1 = frame.OperandStack.PopDouble();
            double v2 = frame.OperandStack.PopDouble();
            frame.OperandStack.PushDouble(v1 - v2);
        }
    }

    //乘法
    class IMUL : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v1 = frame.OperandStack.PopInt32();
            int v2 = frame.OperandStack.PopInt32();
            frame.OperandStack.PushInt32(v1 * v2);
        }
    }
    class LMUL : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long v1 = frame.OperandStack.PopLong();
            long v2 = frame.OperandStack.PopLong();
            frame.OperandStack.PushLong(v1 * v2);
        }
    }
    class FMUL : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            float v1 = frame.OperandStack.PopFloat();
            float v2 = frame.OperandStack.PopFloat();
            frame.OperandStack.PushFloat(v1 * v2);
        }
    }
    class DMUL : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            double v1 = frame.OperandStack.PopDouble();
            double v2 = frame.OperandStack.PopDouble();
            frame.OperandStack.PushDouble(v1 * v2);
        }
    }

    //除法
    class IDIV : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int v1 = frame.OperandStack.PopInt32();
            int v2 = frame.OperandStack.PopInt32();
            if (v2 == 0) throw new DivideByZeroException();
            frame.OperandStack.PushInt32(v2 / v1);
        }
    }
    class LDIV : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long v1 = frame.OperandStack.PopLong();
            long v2 = frame.OperandStack.PopLong();
            if (v2 == 0) throw new DivideByZeroException();
            frame.OperandStack.PushLong(v2 / v1);
        }
    }
    class FDIV : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            float v1 = frame.OperandStack.PopFloat();
            float v2 = frame.OperandStack.PopFloat();
            frame.OperandStack.PushFloat(v2 / v1);
        }
    }
    class DDIV : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            double v1 = frame.OperandStack.PopDouble();
            double v2 = frame.OperandStack.PopDouble();
            frame.OperandStack.PushDouble(v2 / v1);
        }
    }

    //Minus运算
    class INEG : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            int val = frame.OperandStack.PopInt32();
            frame.OperandStack.PushInt32(-val);
        }
    }
    class LNEG : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long val = frame.OperandStack.PopLong();
            frame.OperandStack.PushLong(-val);
        }
    }
    class FNEG : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            float val = frame.OperandStack.PopFloat();
            frame.OperandStack.PushFloat(-val);
        }
    }
    class DNEG : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            double val = frame.OperandStack.PopDouble();
            frame.OperandStack.PushDouble(-val);
        }
    }

    //I自增
    class IINC : Instruction
    {
        public int Index { get; set; }
        public Int32 Const { get; set; }

        public void Execute(Frame frame)
        {
            int val = frame.LocalVariables.GetInt32(Index);
            val += Const;
            frame.LocalVariables.SetInt32(Index, val);
        }

        public void FetchOperands(BytecodeReader reader)
        {
            Index = reader.ReadUInt8();
            Const = reader.ReadInt8();
        }
    }

}
