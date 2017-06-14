using JDRE.JVM.classfile;

namespace JDRE.JVM.runtime.Heap
{
    class ConstantPool
    {
        Class clazz;
        object[] consts;

        public object GetConstant(int index)
        {
            if (consts[index] != null) return consts[index];
            throw new System.NullReferenceException();
        }

        public ConstantPool(Class claz,  classfile.ConstantPool cfCp)
        {
            clazz = claz;
            int count = cfCp.Count;
            consts = new object[count];

            for (ushort i = 0; i < count; i++)
            {
                ConstantInfo info = cfCp.getConstantInfo(i);
                

            }
            
        }

    }
}