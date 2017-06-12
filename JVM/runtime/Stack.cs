using System.Collections.Generic;

namespace JDRE.JVM.runtime
{
    internal class Stack : Stack<Frame>
    {
        int v;
        public Stack(int v) : base(v)
        {
            this.v = v;
        }

        public new void Push(Frame frame)
        {
            if(Count >= v)
            {
                throw new System.StackOverflowException();
            }
            base.Push(frame);
        }

        public new Frame Pop()
        {
            if (base.Count == 0) throw new System.IndexOutOfRangeException();
            return base.Pop();
        }

        public new Frame Peek()
        {
            if (base.Count == 0) throw new System.IndexOutOfRangeException();
            return base.Peek();
        }

        public Frame Top()
        {
            if (base.Count == 0) throw new System.IndexOutOfRangeException();
            return base.Peek();
        }

    }
}