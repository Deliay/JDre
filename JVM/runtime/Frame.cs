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
        public Heap.Method Method { get; private set; }
        public int NextPC { get; set; }

        public Frame(Thread thread, Heap.Method method)
        {
            this.Method = method;
            this.Thread = thread;
            MaxLocals = method.MaxLocals;
            MaxStack = method.MaxStack;
            LocalVariables = new LocalVar(MaxLocals);
            OperandStack = new OperandStack(MaxStack);
        }



    }
}