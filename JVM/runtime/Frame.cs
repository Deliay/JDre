namespace JDRE.JVM.runtime
{
    class Frame
    {
        //Lower for linked list
        public Frame Lower { get; private set; }
        //Paritical variables
        public LocalVar LocalVariables { get; private set; }
        //操作数栈指针
        public OperandStack OperandStack { get; private set; }
        //线程
        public Thread Thread { get; private set; }

        public int MaxLocals { get; private set; }
        public int MaxStack { get; private set; }

        public int NextPC { get; set; }

        public Frame(Thread thread, int maxLocals, int maxStack)
        {
            this.Thread = thread;
            MaxLocals = maxLocals;
            MaxStack = maxStack;
            LocalVariables = new LocalVar(maxLocals);
            OperandStack = new OperandStack(maxStack);
        }



    }
}