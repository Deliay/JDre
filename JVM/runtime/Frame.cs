namespace JDRE.JVM.runtime
{
    public class Frame
    {
        public Frame Lower { get; private set; }
        public LocalVars LocalVariables { get; private set; }
        public OperandStack OperandStack { get; private set; }
        public uint MaxLocals { get; private set; }
        public uint MaxStack { get; private set; }

        public Frame(uint maxLocals, uint maxStack)
        {
            MaxLocals = maxLocals;
            MaxStack = maxStack;
        }



    }
}