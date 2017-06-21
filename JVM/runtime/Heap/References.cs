using JDRE.JVM.classfile;
using JDRE.JVM.exception.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.runtime.Heap
{ 
    class SystemReference
    {
        public ConstantPool Cp = null;
        public string ClassName;
        public Class clazz = null;

        protected SystemReference()
        {

        }

        public Class ResolvedClass()
        {
            if (clazz == null) resolveClassRef();
            return clazz;
        }

        private void resolveClassRef()
        {
            Class d = Cp.Clazz;
            Class c = d.Loader.LoadClass(ClassName);
            if (!c.IsAccessibleTo(d))
            {
                throw new TypeAccessException();
            }
            this.clazz = c;
        }
    }

    class ClassReference : SystemReference
    {
        public ClassReference(ConstantPool cp, ConstantClassInfo classInfo)
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

        protected Method lookupMethodInInterfaces(Class[] interfaces, string name, string descriptor)
        {
            foreach (var item in interfaces)
            {
                foreach (var mt in item.Methods)
                {
                    if (mt.Name == name && mt.Descriptor == descriptor) return mt;
                }

                Method m = lookupMethodInInterfaces(item.Interfaces, name, descriptor);
                if (m != null) return m;
            }
            return null;
        }
    }

    class FieldReference : MemberReference
    {
        public Field field = null;

        public FieldReference(ConstantPool cp, ConstantMemberrefInfo info) : base(info)
        {
            Cp = cp;
        }

        public Field ResolveField()
        {
            if (this.field == null) resolveFieldRef();
            return field;
        }

        private void resolveFieldRef()
        {
            Class d = Cp.Clazz;
            Class c = ResolvedClass();
            field = lookupField(c, Name, Descriptor);

            if (field == null) throw new MissingFieldException();

            if (!field.IsAccessibleTo(d)) throw new FieldAccessException();

        }

        private Field lookupField(Class c, string name, string descriptor)
        {
            foreach (var item in c.Fields)
            {
                if (item.Name == name && item.Descriptor == descriptor) return item;
            }

            foreach (var item in c.Interfaces)
            {
                var f = lookupField(item, name, descriptor);
                if (f != null) return f; 
            }

            if (c.SuperClass != null) return lookupField(c.SuperClass, name, descriptor);

            return null;
        }
    }

    class MethodReference : MemberReference
    {
        public Method method = null;
        public MethodReference(ConstantPool cp, ConstantMemberrefInfo info) : base(info)
        {
            Cp = cp;
        }

        public Method ResolvedMethod()
        {
            if (method == null) resolveMethodRef();
            return method;
        }

        private void resolveMethodRef()
        {
            Class d = Cp.Clazz;
            Class c = ResolvedClass();

            if (c.IsInterface) throw new IncompatiableClassChangeError();

            Method m = lookupMethod(c, Name, Descriptor);

            if (m == null) throw new NoSuchMethodError();

            if (!m.IsAccessibleTo(d)) throw new IllegalAccessError();

            method = m;
        }

        private Method lookupMethod(Class c, string name, string descriptor)
        {
            Method m = LookupMethodInClass(c, name, descriptor);

            if (m == null) m = lookupMethodInInterfaces(c.Interfaces, name, descriptor);

            return m;
        }

        public static Method LookupMethodInClass(Class c, string name, string descriptor)
        {
            for (Class clz = c; c != null; clz = clz.SuperClass)
            {
                foreach (var item in clz.Methods)
                {
                    if (item.Name == name && item.Descriptor == descriptor) return item;
                }
            }

            return null;
        }
    }

    class InterfaceMethodReference : MemberReference
    {
        public Method method = null;

        public InterfaceMethodReference(ConstantPool cp, ConstantMemberrefInfo info) : base(info)
        {
            Cp = cp;
        }

        public Method ResolvedMethod()
        {
            if (method == null) resolveInterfaceMethodRef();
            return method;
        }

        private void resolveInterfaceMethodRef()
        {
            Class d = Cp.Clazz;
            Class c = ResolvedClass();

            if (c.IsInterface) throw new IncompatiableClassChangeError();

            Method mm = lookupInterfaceMethod(c, Name, Descriptor);

            if (mm == null) throw new NoSuchMethodError();

            if (!mm.IsAccessibleTo(d)) throw new IllegalAccessError();

            method = mm;
        }

        private Method lookupInterfaceMethod(Class c, string name, string descriptor)
        {
            foreach (var item in c.Methods)
            {
                if (item.Name == name && item.Descriptor == descriptor) return item;
            }

            return lookupMethodInInterfaces(c.Interfaces, name, descriptor);
        }

    }
}
