using JDRE.JVM.classfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.runtime.Heap
{
    class ClassMember
    {
        UInt16 accessFlag;
        string name;
        string descriptor;
        public Class clazz;

        protected ClassMember(MemberInfo info)
        {
            accessFlag = info.accessflag;
            name = info.Name();
            descriptor = info.Descriptor();
        }
    }
}
