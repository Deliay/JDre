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
        int accessFlag;
        string name;
        string descriptor;
        public Class Clazz;

        public int AccessFlag { get => accessFlag; }
        public bool IsPublic { get => (accessFlag & (int)Heap.AccessFlag.ACC_PUBLIC) != 0; }
        public bool IsPrivate { get => (accessFlag & (int)Heap.AccessFlag.ACC_PRIVATE) != 0; }
        public bool IsProtected { get => (accessFlag & (int)Heap.AccessFlag.ACC_PROTECTED) != 0; }
        public bool IsFinal { get => (accessFlag & (int)Heap.AccessFlag.ACC_FINAL) != 0; }
        public bool IsSynthetic { get => (accessFlag & (int)Heap.AccessFlag.ACC_SYNTHETIC) != 0; }
        public bool IsStatic { get => (accessFlag & (int)Heap.AccessFlag.ACC_STATIC) != 0; }

        public string Name { get => name; }
        public string Descriptor { get => descriptor; }

        protected ClassMember(MemberInfo info)
        {
            accessFlag = info.accessflag;
            name = info.Name();
            descriptor = info.Descriptor();
        }

        public bool IsAccessibleTo(Class d)
        {
            if (IsPublic) return true;
            if (IsProtected)
            {
                return d == Clazz || d.isSubClassOf(Clazz) || Clazz.getPackageName() == d.getPackageName(); 
            }
            if (!IsPrivate)
            {
                return Clazz.getPackageName() == d.getPackageName();
            }
            return Clazz == d;
        }
    }
}
