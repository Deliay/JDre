using JDRE.JVM.runtime.Heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.runtime
{
    class Object
    {
        Class clazz;
        Slots fields;

        public Object(Class clazz)
        {
            this.clazz = clazz;
            fields = new Slots(clazz.InstanceSlotCount);
        }

        public Class Clazz { get => clazz; }
        public Slots Fields { get => fields; }

        public bool IsInstanceOf(Class clazz)
        {
        }
    }
}
