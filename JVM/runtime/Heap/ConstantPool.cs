using JDRE.JVM.classfile;

namespace JDRE.JVM.runtime.Heap
{
    class ConstantPool
    {
        Class clazz;
        object[] consts;

        public Class Clazz { get => clazz; }

        public object GetConstant(int index)
        {
            if (consts[index] != null) return consts[index];
            throw new System.NullReferenceException();
        }

        public ConstantPool(Class clazz,  classfile.ConstantPool cfCp)
        {
            this.clazz = clazz;
            int count = cfCp.Count;
            consts = new object[count];

            for (ushort i = 1; i < count; i++)
            {
                ConstantInfo info = cfCp.getConstantInfo(i);
                
                if(info is ConstantIntegerInfo)
                {
                    consts[i] = ((ConstantIntegerInfo)info).value;
                }
                else if (info is ConstantFloatInfo)
                {
                    consts[i] = ((ConstantFloatInfo)info).value;
                }
                else if (info is ConstantLongInfo)
                {
                    consts[i] = ((ConstantLongInfo)info).value;
                    i++;
                }
                else if (info is ConstantDoubleInfo)
                {
                    consts[i] = ((ConstantLongInfo)info).value;
                    i++;
                }
                else if (info is ConstantStringInfo)
                {
                    consts[i] = ((ConstantStringInfo)info).ToString();
                }
                else if (info is ConstantClassInfo)
                {
                    consts[i] = new ClassReference(this, info as ConstantClassInfo);
                }
                else if (info is ConstantFieldrefInfo)
                {
                    consts[i] = new FieldReference(this, info as ConstantFieldrefInfo);
                }
                else if (info is ConstantMethodrefInfo)
                {
                    consts[i] = new MethodReference(this, info as ConstantMethodrefInfo);
                }
                else if (info is ConstantInterfaceMethodrefInfo)
                {
                    consts[i] = new InterfaceMethodReference(this, info as ConstantInterfaceMethodrefInfo);
                }
            }
            
        }

    }
}