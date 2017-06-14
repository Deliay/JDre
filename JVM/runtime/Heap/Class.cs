using System;

namespace JDRE.JVM.runtime.Heap
{
    class Class
    {
        UInt16 accessFlag;
        string name;
        string superClassName;
        string[] interfaceNames;
        ConstantPool constantPool;
        Field[] fields;
        Method[] methods;
        ClassLoader loader;
        Class superClass;
        Class[] interfaces;
        int instanceSoltCount;
        int staticSoltCount;
        Solts staticVars;
    }

}
