using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDRE.JVM.runtime;
using JDRE.JVM.instructions.Loads;
using JDRE.JVM.instructions.Stores;
using JDRE.JVM.instructions.Math;
using JDRE.JVM.instructions.Base;

namespace JDRE.JVM.instructions.Extended
{
    //对单字节指令进行扩展
    class WIDE : Instruction
    {
        Instruction inc;

        public void Execute(Frame frame)
        {
            inc.Execute(frame);
        }

        public void FetchOperands(BytecodeReader reader)
        {
            byte opcode = reader.ReadUInt8();
            switch (opcode)
            {
                //原本FetchOperands只取1字节，这里取2字节
                case 0x15: inc = new ILOAD() { Index = reader.ReadUInt16() }; break;
                case 0x16: inc = new LLOAD() { Index = reader.ReadUInt16() }; break;
                case 0x17: inc = new FLOAD() { Index = reader.ReadUInt16() }; break;
                case 0x18: inc = new DLOAD() { Index = reader.ReadUInt16() }; break;
                case 0x19: inc = new ALOAD() { Index = reader.ReadUInt16() }; break;
                case 0x36: inc = new ISTORE() { Index = reader.ReadUInt16() }; break;
                case 0x37: inc = new LSTORE() { Index = reader.ReadUInt16() }; break;
                case 0x38: inc = new FSTORE() { Index = reader.ReadUInt16() }; break;
                case 0x39: inc = new DSTORE() { Index = reader.ReadUInt16() }; break;
                case 0x3a: inc = new ASTORE() { Index = reader.ReadUInt16() }; break;
                case 0x84: inc = new IINC() { Index = reader.ReadUInt16(), Const = reader.ReadUInt16() }; break;
                case 0xa9:
                default:
                    throw new Exception("Unsupport opcode: 0xa9");
            }
        } 
    }

    //栈顶对象判断是否为NULL
    class IFNULL : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            runtime.Object obj = frame.OperandStack.PopObject();
            if(obj == null)
            {
                BranchJump(frame, Offset);
            }
        }
    }
    class IFNONNULL : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            runtime.Object obj = frame.OperandStack.PopObject();
            if (obj != null)
            {
                BranchJump(frame, Offset);
            }
        }
    }

    //4字节goto
    class GOTO_W : Instruction
    {
        int offset;
        public void FetchOperands(BytecodeReader reader)
        {
            offset = reader.ReadInt32();
        }

        public void Execute(Frame frame)
        {
            BranchInstruction.BranchJump(frame, offset);
        }
    }
}
