using JDRE.JVM.classfile;

namespace JDRE.JVM.runtime.Heap
{
    class Field : ClassMember
    {
        public int SlotId;
        public int ConstValueIndex;

        public bool IsVolatile { get => (AccessFlag & (int)Heap.AccessFlag.ACC_VOLATILE) != 0; }
        public bool IsTransient { get => (AccessFlag & (int)Heap.AccessFlag.ACC_TRANSIENT) != 0; }
        public bool IsEnum { get => (AccessFlag & (int)Heap.AccessFlag.ACC_ENUM) != 0; }

        public static Field[] CreateFields(Class clazz, MemberInfo[] cfFields)
        {
            Field[] fields = new Field[cfFields.Length];
            for (int i = 0; i < cfFields.Length; i++)
            {
                fields[i] = new Field(cfFields[i]) { Clazz = clazz};
                
            }
            return fields;
        }

        public Field(MemberInfo info) : base(info)
        {
            CodeAttribute ca = info.CodeAttr();
            if (ca != null)
            {
                ConstValueIndex = ca.ConstantValueIndex().Index;
            }
        }

        public bool IsLongOrDouble()
        {
            return Descriptor == "J" || Descriptor == "D";
        }
    }
}