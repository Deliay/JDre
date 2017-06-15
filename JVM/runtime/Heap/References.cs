using JDRE.JVM.classfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.runtime.Heap
{ 
    class SystemReference
    {
        public classfile.ConstantPool Cp;
        public string ClassName;
        public Class clazz;

        protected SystemReference() { }
    }

    class ClassReference : SystemReference
    {
        public ClassReference(classfile.ConstantPool cp, ConstantClassInfo classInfo)
        {
            Cp = cp;
            ClassName = classInfo.ToString();
        }
    }

    class MemberReference : SystemReference
    {
        public string Name;
        public string Descriptor;
        public MemberReference(ConstantMemberrefInfo info)
        {
            base.ClassName = info.ClassName();
            info.NameAndDescriptor(out Name, out Descriptor);
        }
    }

    class FieldReference : MemberReference
    {
        public Field field;

        public FieldReference(classfile.ConstantPool cp, ConstantMemberrefInfo info) : base(info)
        {
            Cp = cp;
        }
    }

    class MethodReference : MemberReference
    {
        public Method method;
        public MethodReference(classfile.ConstantPool cp, ConstantMemberrefInfo info) : base(info)
        {
            Cp = cp;
        }
    }

    class InterfaceMethodReference : MemberReference
    {
        public Method method;

        public InterfaceMethodReference(classfile.ConstantPool cp, ConstantMemberrefInfo info) : base(info)
        {
            Cp = cp;
        }
    }
}
