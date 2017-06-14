using JDRE.JVM.classfile;

namespace JDRE.JVM.runtime.Heap
{
    class Field : ClassMember
    {
        public static Field[] CreateFields(Class clazz, MemberInfo[] cfFields)
        {
            Field[] fields = new Field[cfFields.Length];
            for (int i = 0; i < cfFields.Length; i++)
            {
                fields[i] = new Field(cfFields[i]) { clazz = clazz};
            }
            return fields;
        }

        public Field(MemberInfo info) : base(info)
        {

        }
    }
}