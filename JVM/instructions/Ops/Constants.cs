using JDRE.JVM.instructions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDRE.JVM.runtime;
using JDRE.JVM.exception.Lang;

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

    class LDC : Index8Instruction
    {
        public static void _LDC(Frame frame, int index)
        {
            OperandStack stack = frame.OperandStack;
            runtime.Heap.ConstantPool cp= frame.Method.Clazz.HeapConstants;
            object c = cp.GetConstant(index);

            if (c is Int32) stack.PushInt32((Int32)c);
            else if (c is float) stack.PushFloat((float)c);
            //else if (c is string) .......
            //else if (c is heap.ClassReference) .......
            else throw new Exception("LDC Error");

        }

        public override void Execute(Frame frame)
        {
            _LDC(frame, Index);
        }
    }
    class LDC_W : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            LDC._LDC(frame, Index);
        }
    }
    class LDC2_W : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            OperandStack stack = frame.OperandStack;
            runtime.Heap.ConstantPool cp = frame.Method.Clazz.HeapConstants;
            object c = cp.GetConstant(Index);

            if (c is Int64) stack.PushLong((Int64)c);
            else if (c is Double) stack.PushDouble((Double)c);
            else throw new ClsasFormatError();
        }
    }

}
