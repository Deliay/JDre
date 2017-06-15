﻿using JDRE.JVM.classfile;

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
                    consts[i] = new ClassReference(cfCp, info as ConstantClassInfo);
                }
                else if (info is ConstantFieldrefInfo)
                {
                    consts[i] = new FieldReference(cfCp, info as ConstantFieldrefInfo);
                }
                else if (info is ConstantMethodrefInfo)
                {
                    consts[i] = new MethodReference(cfCp, info as ConstantMethodrefInfo);
                }
                else if (info is ConstantInterfaceMethodrefInfo)
                {
                    consts[i] = new InterfaceMethodReference(cfCp, info as ConstantInterfaceMethodrefInfo);
                }
            }
            
        }

    }
}