using System;
using System.Collections.Generic;

namespace JDRE.JVM.runtime
{

    class LocalVar : List<Solt>
    {
        public LocalVar(int maxLocal) : base(maxLocal)
        {
            for (int i = 0; i < maxLocal; i++)
            {
                this.Add(new Solt());
            }
        }

        public void SetInt32(int index, Int32 value)
        {
            base[index].Number = value;
        }
        public Int32 GetInt32(int index)
        {
            return (Int32)base[index].Number;
        }

        public void SetFloat(int index, float value)
        {
            base[index].Number = (Int32)value;
        }

        public Single GetFloat(int index)
        {
            return (Single)base[index].Number;
        }

        public void SetLong(int index, Int64 value)
        {
            this[index].Number = value;

        }

        public Int64 GetLong(int index)
        {
            return (Int64)(this[index].Number);
        }

        public void SetDouble(int index, Double value)
        {
            this[index].Number = value;
        }

        public Double GetDouble(int index)
        {
            return (Double)this[index].Number;
        }

        public Double GetValue(int index)
        {
            return (Double)this[index].Number;
        }

        public void SetRefer(int index, Object obj)
        {
            base[index].Refer = obj;
        }

        public Object GetRefer(int index)
        {
            return base[index].Refer;
        }
    }
}