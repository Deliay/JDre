using JDRE.JVM.instructions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDRE.JVM.runtime;

namespace JDRE.JVM.instructions.Comparisions
{ 
    //比较long
    class LCMP : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            long v2 = frame.OperandStack.PopLong();
            long v1 = frame.OperandStack.PopLong();
            int result;
            if (v1 > v2)
            {
                result = 1;
            }
            else if (v1 == v2)
            {
                result = 0;
            }
            else
            {
                result = -1;
            }
            frame.OperandStack.PushInt32(result);
        }
    }

    //FCMP<op> DCMP<OP>
    class FCMPG : NoOperandsInstruction
    {
        //浮点运算可能产生NaN的结果，两者有NaN时，无法比较，用flag来表示
        public static void _FCMP(Frame f, bool flag)
        {
            float v2 = f.OperandStack.PopFloat();
            float v1 = f.OperandStack.PopFloat();
            int result;
            if (v1 > v2) result = 1;
            else if (v1 == v2) result = 0;
            else if (v1 < v2) result = -1;
            else if (flag) result = 1;
            else result = 1;

            f.OperandStack.PushInt32(result);
        }

        public override void Execute(Frame frame)
        {
            _FCMP(frame, true);
        }
    }
    class FCMPL : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            FCMPG._FCMP(frame, false);
        }
    }
    class DCMPG : NoOperandsInstruction
    {
        //浮点运算可能产生NaN的结果，两者有NaN时，无法比较，用flag来表示
        public static void _DCMP(Frame f, bool flag)
        {
            double v2 = f.OperandStack.PopDouble();
            double v1 = f.OperandStack.PopDouble();
            int result;
            if (v1 > v2) result = 1;
            else if (v1 == v2) result = 0;
            else if (v1 < v2) result = -1;
            else if (flag) result = 1;
            else result = 1;

            f.OperandStack.PushInt32(result);
        }

        public override void Execute(Frame frame)
        {
            _DCMP(frame, true);
        }
    }
    class DCMPL : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            DCMPG._DCMP(frame, false);
        }
    }

    //IF<COND> 指令
    //将操作数栈顶int变量弹出和0进行比较
    class IFEQ : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int val = frame.OperandStack.PopInt32();
            if (val == 0) BranchJump(frame, Offset);
        }
    }
    class IFNE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int val = frame.OperandStack.PopInt32();
            if (val != 0) BranchJump(frame, Offset);
        }
    }
    class IFLT : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int val = frame.OperandStack.PopInt32();
            if (val < 0) BranchJump(frame, Offset);
        }
    }
    class IFLE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int val = frame.OperandStack.PopInt32();
            if (val <= 0) BranchJump(frame, Offset);
        }
    }
    class IFGT : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int val = frame.OperandStack.PopInt32();
            if (val > 0) BranchJump(frame, Offset);
        }
    }
    class IFGE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int val = frame.OperandStack.PopInt32();
            if (val >= 0) BranchJump(frame, Offset);
        }
    }

    //IF_ICMP<COND>  指令
    //将操作数栈顶两个int变量弹出，进行比较
    class IF_ICMPEQ : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();

            if (v1 == v2) BranchJump(frame, Offset);
        }
    }
    class IF_ICMPNE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();

            if (v1 != v2) BranchInstruction.BranchJump(frame, Offset);
        }
    }
    class IF_ICMPLT : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();

            if (v1 < v2) BranchInstruction.BranchJump(frame, Offset);
        }
    }
    class IF_ICMPLE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();

            if (v1 <= v2) BranchInstruction.BranchJump(frame, Offset);
        }
    }
    class IF_ICMPGT : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();

            if (v1 > v2) BranchInstruction.BranchJump(frame, Offset);
        }
    }
    class IF_ICMPGE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            int v2 = frame.OperandStack.PopInt32();
            int v1 = frame.OperandStack.PopInt32();

            if (v1 >= v2) BranchInstruction.BranchJump(frame, Offset);
        }
    }

    //IF_ACMP<COND> 指令
    //将栈顶两个引用弹出，判定是否同引用
    class IF_ACMPEQ : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            runtime.Object ref1 = frame.OperandStack.PopObject();
            runtime.Object ref2 = frame.OperandStack.PopObject();
            if (ref1 == ref2) BranchJump(frame, Offset);
        }
    }
    class IF_ACMPNE : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            runtime.Object ref1 = frame.OperandStack.PopObject();
            runtime.Object ref2 = frame.OperandStack.PopObject();
            if (ref1 != ref2) BranchJump(frame, Offset);
        }
    }
}
