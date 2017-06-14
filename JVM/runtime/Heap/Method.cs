using JDRE.JVM.classfile;

namespace JDRE.JVM.runtime.Heap
{
    class Method : ClassMember
    {
        int maxStack;
        int maxLocals;
        byte[] code;

        public static Method[] CreateMethods(Class clazz, MemberInfo[] cfMethods)
        {
            Method[] methods = new Method[cfMethods.Length];
            for (int i = 0; i < cfMethods.Length; i++)
            {
                methods[i] = new Method(cfMethods[i]) { clazz = clazz };
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