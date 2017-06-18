using System;
using JDRE.JVM.classfile;
using System.Linq;

namespace JDRE.JVM.runtime.Heap
{
    class Class
    {
        int accessFlag;
        public string Name;

        public string SuperClassName;
        public Class SuperClass;

        ConstantPool constantPool;

        public Field[] Fields;
        Method[] methods;

        public string[] InterfaceNames;
        public Class[] Interfaces = null;

        public int InstanceSlotCount;
        public int StaticSlotCount;

        public Slots StaticVars;

        private ClassFile cf;

        public bool isAssignableForm(Class clazz)
        {
            Class s = clazz, t = this;
            if (s == t) return true;
            if (!t.IsInterface) return s.isSubClassOf(t);
            else return s.isImplements(t);
        }

        public bool isImplements(Class iface)
        {
            for (Class super = this; super != null; super = super.SuperClass)
            {
                foreach (var item in super.Interfaces)
                {
                    if (item == iface || item.isSubInterfaceOf(iface)) return true;
                }
            }
            return false;
        }

        private bool isSubInterfaceOf(Class iface)
        {
            foreach (var item in this.Interfaces)
            {
                if (item == iface || item.isSubInterfaceOf(iface)) return true;
            }

            return false;
        }

        public ClassLoader Loader;

        public Class(ClassFile cf)
        {
            this.cf = cf;
            this.accessFlag = cf.AccessFlags;
            this.Name = cf.ClassName;
            this.SuperClassName = cf.SuperClassName;
            this.InterfaceNames = cf.InterfaceNames().ToArray();
            this.constantPool = new ConstantPool(this, cf.Constats);
            this.Fields = Field.CreateFields(this, cf.Fields.ToArray());
            this.methods = Method.CreateMethods(this, cf.Methods.ToArray());
            //this.StaticVars = new Slots(this.StaticSlotCount);
        }

        public bool IsPublic { get => (accessFlag & (int)AccessFlag.ACC_PUBLIC) != 0; }
        public bool IsFinal { get => (accessFlag & (int)AccessFlag.ACC_FINAL) != 0; }
        public bool IsSuper { get => (accessFlag & (int)AccessFlag.ACC_SUPER) != 0; }

        public bool isSubClassOf(Class other)
        {
            for (Class super = this.SuperClass; super != null; super = super.SuperClass)
            {
                if (other == super) return true;
            }

            return false;
        }

        public bool IsInterface { get => (accessFlag & (int)AccessFlag.ACC_INTERFACE) != 0; }
        public bool IsAbstract { get => (accessFlag & (int)AccessFlag.ACC_ABSTRACT) != 0; }
        public bool IsSynthetic { get => (accessFlag & (int)AccessFlag.ACC_SYNTHETIC) != 0; }
        public bool IsAnnotation { get => (accessFlag & (int)AccessFlag.ACC_ANNOTATION) != 0; }
        public bool IsEnum { get => (accessFlag & (int)AccessFlag.ACC_ENUM) != 0; }
        public ConstantPool HeapConstants { get => constantPool; }
        public string getPackageName()
        {
            if (Name.LastIndexOf('/') >= 0) return Name;
            return "";
        }

        public bool IsAccessibleTo(Class other) { return IsPublic || getPackageName() == other.getPackageName(); }

        public Method getMainMethod()
        {
            return getStaticMethod("main", "([Ljava/lang/String;)V");
        }

        public Method getStaticMethod(string name, string descriptor)
        {
            foreach (var item in methods)
            {
                if(item.IsStatic && item.Name == name && item.Descriptor == descriptor)
                {
                    return item;
                }
            }
            return null;
        }

        public Object ToObject()
        {
            return new Object(this);
        }
    }

}
