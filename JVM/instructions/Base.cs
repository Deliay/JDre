using JDRE.JVM.runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.instructions.Base
{

    interface Instruction
    {
        void FetchOperands(BytecodeReader reader);
        void Execute(Frame frame);
    }

    class NoOperandsInstruction : Instruction
    {
        public virtual void Execute(Frame frame)
        {

        }

        public virtual void FetchOperands(BytecodeReader reader)
        {

        }
    };

    class BranchInstruction : Instruction
    {
        public int Offset { get; private set; }

        public static void BranchJump(Frame frame, int offset)
        {
            frame.NextPC = frame.Thread.PC + offset;
        }

        public virtual void Execute(Frame frame)
        {

        }

        public void FetchOperands(BytecodeReader reader)
        {
            Offset = reader.ReadInt16();
        }
    }

    class Index8Instruction : Instruction
    {
        public int Index { get; set; }

        public virtual void Execute(Frame frame)
        {

        }

        public void FetchOperands(BytecodeReader reader)
        {
            Index = (int)reader.ReadUInt8();
        }
    }

    class Index16Instruction : Instruction
    {
        public int Index { get; private set; }

        public virtual void Execute(Frame frame)
        {

        }

        public void FetchOperands(BytecodeReader reader)
        {
            Index = reader.ReadUInt16();
        }
    }
}
