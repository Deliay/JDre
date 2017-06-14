using JDRE.JVM.instructions.Base;
using JDRE.JVM.runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.instructions.Stores
{ 
    class LSTORE : Index8Instruction
    {
        public static void Store(Frame f, int index)
        {
            long val = f.OperandStack.PopLong();
            f.LocalVariables.SetLong(index, val);
        }

        public override void Execute(Frame frame)
        {
            Store(frame, Index);
        }
    }
    class LSTORE_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            LSTORE.Store(frame, 0);
        }
    }
    class LSTORE_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            LSTORE.Store(frame, 1);
        }
    }
    class LSTORE_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            LSTORE.Store(frame, 2);
        }
    }
    class LSTORE_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            LSTORE.Store(frame, 3);
        }
    }

    class ISTORE : Index8Instruction
    {
        public static void Store(Frame f, int index)
        {
            int val = f.OperandStack.PopInt32();
            f.LocalVariables.SetInt32(index, val);
        }

        public override void Execute(Frame frame)
        {
            Store(frame, Index);
        }
    }
    class ISTORE_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ISTORE.Store(frame, 0);
        }
    }
    class ISTORE_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ISTORE.Store(frame, 1);
        }
    }
    class ISTORE_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ISTORE.Store(frame, 2);
        }
    }
    class ISTORE_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ISTORE.Store(frame, 3);
        }
    }

    class FSTORE : Index8Instruction
    {
        public static void Store(Frame f, int index)
        {
            float val = f.OperandStack.PopFloat();
            f.LocalVariables.SetFloat(index, val);
        }

        public override void Execute(Frame frame)
        {
            Store(frame, Index);
        }
    }
    class FSTORE_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            FSTORE.Store(frame, 0);
        }
    }
    class FSTORE_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            FSTORE.Store(frame, 1);
        }
    }
    class FSTORE_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            FSTORE.Store(frame, 2);
        }
    }
    class FSTORE_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            FSTORE.Store(frame, 3);
        }
    }

    class DSTORE : Index8Instruction
    {
        public static void Store(Frame f, int index)
        {
            double val = f.OperandStack.PopDouble();
            f.LocalVariables.SetDouble(index, val);
        }

        public override void Execute(Frame frame)
        {
            Store(frame, Index);
        }
    }
    class DSTORE_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            DSTORE.Store(frame, 0);
        }
    }
    class DSTORE_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            DSTORE.Store(frame, 1);
        }
    }
    class DSTORE_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            DSTORE.Store(frame, 2);
        }
    }
    class DSTORE_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            DSTORE.Store(frame, 3);
        }
    }

    class ASTORE : Index8Instruction
    {
        public static void Store(Frame f, int index)
        {
            runtime.Object val = f.OperandStack.PopObject();
            f.LocalVariables.SetRefer(index, val);
        }

        public override void Execute(Frame frame)
        {
            Store(frame, Index);
        }
    }
    class ASTORE_0 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ASTORE.Store(frame, 0);
        }
    }
    class ASTORE_1 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ASTORE.Store(frame, 1);
        }
    }
    class ASTORE_2 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ASTORE.Store(frame, 2);
        }
    }
    class ASTORE_3 : NoOperandsInstruction
    {
        public override void Execute(Frame frame)
        {
            ASTORE.Store(frame, 3);
        }
    }
}
