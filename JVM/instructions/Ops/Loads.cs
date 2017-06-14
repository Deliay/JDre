using JDRE.JVM.instructions.Base;
using JDRE.JVM.runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.instructions.Loads
{
    class ILOAD : Index8Instruction
    {
        public static void Load(Frame f, int index)
        {
            int val = f.LocalVariables.GetInt32(index);
            f.OperandStack.PushInt32(val);
        }

        public override void Execute(Frame frame)
        {
            Load(frame, base.Index);
        }
    }
    class ILOAD_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ILOAD.Load(frame, 0);
        }
    }
    class ILOAD_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ILOAD.Load(frame, 1);
        }
    }
    class ILOAD_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ILOAD.Load(frame, 2);
        }
    }
    class ILOAD_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ILOAD.Load(frame, 3);
        }
    }

    class LLOAD : Index8Instruction
    {
        public static void Load(Frame f, int index)
        {
            long val = f.LocalVariables.GetLong(index);
            f.OperandStack.PushLong(val);
        }

        public override void Execute(Frame frame)
        {
            Load(frame, base.Index);
        }
    }
    class LLOAD_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            LLOAD.Load(frame, 0);
        }
    }
    class LLOAD_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            LLOAD.Load(frame, 1);
        }
    }
    class LLOAD_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            LLOAD.Load(frame, 2);
        }
    }
    class LLOAD_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            LLOAD.Load(frame, 3);
        }
    }

    class FLOAD : Index8Instruction
    {
        public static void Load(Frame f, int index)
        {
            float val = f.LocalVariables.GetFloat(index);
            f.OperandStack.PushFloat(val);
        }

        public override void Execute(Frame frame)
        {
            Load(frame, base.Index);
        }
    }
    class FLOAD_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            FLOAD.Load(frame, 0);
        }
    }
    class FLOAD_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            FLOAD.Load(frame, 1);
        }
    }
    class FLOAD_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            FLOAD.Load(frame, 2);
        }
    }
    class FLOAD_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            FLOAD.Load(frame, 3);
        }
    }

    class DLOAD : Index8Instruction
    {
        public static void Load(Frame f, int index)
        {
            double val = f.LocalVariables.GetDouble(index);
            f.OperandStack.PushDouble(val);
        }

        public override void Execute(Frame frame)
        {
            Load(frame, base.Index);
        }
    }
    class DLOAD_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            DLOAD.Load(frame, 0);
        }
    }
    class DLOAD_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            DLOAD.Load(frame, 1);
        }
    }
    class DLOAD_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            DLOAD.Load(frame, 2);
        }
    }
    class DLOAD_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            DLOAD.Load(frame, 3);
        }
    }

    class ALOAD : Index8Instruction
    {
        public static void Load(Frame f, int index)
        {
            runtime.Object val = f.LocalVariables.GetRefer(index);
            f.OperandStack.PushObject(val);
        }

        public override void Execute(Frame frame)
        {
            Load(frame, base.Index);
        }
    }
    class ALOAD_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ALOAD.Load(frame, 0);
        }
    }
    class ALOAD_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ALOAD.Load(frame, 1);
        }
    }
    class ALOAD_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ALOAD.Load(frame, 2);
        }
    }
    class ALOAD_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ALOAD.Load(frame, 3);
        }
    }
}
