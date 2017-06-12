using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.runtime
{
    class Thread
    {
        public static Thread CreateThread()
        {
            return new Thread() { stack = new Stack(1024) };
        }

        Stack stack;
        public int PC { get; set; }
        public void PushFrame(Frame frame)
        {
            stack.Push(frame);
        }

        public Frame PopFrame()
        {
            return stack.Pop();
        }

        public Frame CurrentFrame()
        {
            return stack.Peek();
        }
    }
}
