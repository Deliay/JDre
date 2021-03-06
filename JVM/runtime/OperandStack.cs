﻿using System;
using System.Collections.Generic;

namespace JDRE.JVM.runtime
{
    class OperandStack : Stack<Slot>
    {
        public OperandStack(int maxStack) : base(maxStack)
        {
        }

        public void PushInt32(Int32 value)
        {
            base.Push(new Slot() { Number = value });
        }

        public Int32 PopInt32()
        {
            return (Int32)base.Pop().Number;
        }

        public void PushFloat(float value)
        {
            base.Push(new Slot() { Number = value });
        }

        public Single PopFloat()
        {
            return (Single)base.Pop().Number;
        }

        public void PushLong(Int64 value)
        {
            base.Push(new Slot() { Number = (Int32)value });
        }

        public Int64 PopLong()
        {
            return (Int64)base.Pop().Number;
            
        }

        public void PushDouble(Double value)
        {
            base.Push(new Slot() { Number = value });
        }

        public Double PopDouble()
        {
            return (Double)base.Pop().Number;
        }

        public Object PopObject()
        {
            return base.Pop().Refer;
        }

        public void PushObject(Object obj)
        {
            base.Push(new Slot() { Refer = obj });
        }

        public Object GetObjectFromTop(int n)
        {
            return ToArray()[Count - 1 - n].Refer;
        }
    }
}