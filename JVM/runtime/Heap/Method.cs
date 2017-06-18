using JDRE.JVM.classfile;
using System.IO;

namespace JDRE.JVM.runtime.Heap
{
    class Method : ClassMember
    {
        int maxStack;
        int maxLocals;
        byte[] code;

        public bool IsBridge { get => (AccessFlag & (int)Heap.AccessFlag.ACC_BRIDGE) != 0; }
        public bool IsVarargs { get => (AccessFlag & (int)Heap.AccessFlag.ACC_VARARGS) != 0; }
        public bool IsNative { get => (AccessFlag & (int)Heap.AccessFlag.ACC_NATIVE) != 0; }
        public bool IsAbstract { get => (AccessFlag & (int)Heap.AccessFlag.ACC_ABSTRACT) != 0; }
        public bool IsStrict { get => (AccessFlag & (int)Heap.AccessFlag.ACC_STRICT) != 0; }
        public bool IsSynchronized { get => (AccessFlag & (int)Heap.AccessFlag.ACC_SYNCHRONIZED) != 0; }

        public int MaxStack { get => maxStack; }
        public int MaxLocals { get => maxLocals; }

        public byte[] Code { get => code; }

        public static Method[] CreateMethods(Class clazz, MemberInfo[] cfMethods)
        {
            Method[] methods = new Method[cfMethods.Length];
            for (int i = 0; i < cfMethods.Length; i++)
            {
                methods[i] = new Method(cfMethods[i]) { Clazz = clazz };
            }
            return methods;
        }

        public Method(MemberInfo info) : base(info)
        {
            CodeAttribute attr = info.CodeAttr();
            if (attr != null)
            {
                maxStack = attr.maxStack;
                maxLocals = attr.maxLocals;
                code = attr.code;
            }
        }
    }
}