using System;
using System.Collections.Generic;
using System.IO;

namespace JDRE.JVM.runtime.Heap
{
    class ClassLoader
    {
        classpath.Classpath cp;
        Dictionary<string, Class> classMap;

        public ClassLoader(classpath.Classpath cp)
        {
            this.cp = cp;
            classMap = new Dictionary<string, Class>();
        }

        public Class LoadClass(string name)
        {
            Class result;
            if(classMap.TryGetValue(name, out result))
            {
                return result;
            }
            return loadNonArrayClass(name);
        }

        private Class loadNonArrayClass(string name)
        {
            readClass(name, out Stream d, out classpath.Entry e);
            return defineClass(d);
        }

        private Class defineClass(Stream data)
        {
            classfile.ClassFile cf = new classfile.ClassFile(data);
            cf.Read();
            Class clazz = new Class(cf)
            {
                Loader = this
            };
            resolveSuperClass(clazz);
            return clazz;
        }

        private void resolveSuperClass(Class clazz)
        {
            if(clazz.Name != "java/lang/Objet")
            {
                clazz.SuperClass = clazz.Loader.LoadClass(clazz.SuperClassName);
            }
        }

        private void resolveInterface(Class clazz)
        {
            int count = clazz.InterfaceNames.Length;
            if (count > 0)
            {
                Class[] interfaces = new Class[count];
                for (int i = 0; i < count; i++)
                {
                    interfaces[i] = clazz.Loader.LoadClass(clazz.InterfaceNames[i]);
                }
            }
        }

        private void LinkedList(Class clazz)
        {
            verify(clazz);
            prepare(clazz);
        }

        private void prepare(Class clazz)
        {
            calcInstanceFieldSlotIds(clazz);
            clacStaticFieldSlotIds(clazz);
            allocAndInitStaticVars(clazz);
        }

        private void allocAndInitStaticVars(Class clazz)
        {
            clazz.StaticVars = new Slots(clazz.StaticSlotCount);
            foreach (var item in clazz.Fields)
            {
                if(item.IsStatic && item.IsFinal)
                {
                    initStaticFinalVar(clazz, item);
                }
            }
        }

        private void initStaticFinalVar(Class clazz, Field field)
        {
            Slots vars = clazz.StaticVars;
            ConstantPool cp = clazz.HeapConstants;
            int cpIndex = field.ConstValueIndex;
            int slotId = field.SlotId;
            
            if(cpIndex > 0)
            {
                switch (field.Descriptor)
                {
                    case "Z":
                    case "B":
                    case "C":
                    case "S":
                    case "I":
                        int ival = (int)cp.GetConstant(cpIndex);
                        vars.SetInt32(slotId, ival);
                        break;
                    case "J":
                        long lval = (long)cp.GetConstant(cpIndex);
                        vars.SetLong(slotId, lval);
                        cpIndex++;
                        break;
                    case "F":
                        float fval = (float)cp.GetConstant(cpIndex);
                        vars.SetFloat(cpIndex, fval);
                        break;
                    case "D":
                        double dval = (double)cp.GetConstant(cpIndex);
                        vars.SetDouble(cpIndex, dval);
                        cpIndex++;
                        break;
                    case "Ljava/lang/String":
                        throw new NotImplementedException();
                    default:
                        break;
                }
            }
        }

        private void clacStaticFieldSlotIds(Class clazz)
        {
            int slotId = 0;

            foreach (var item in clazz.Fields)
            {
                if (item.IsStatic)
                {
                    item.SlotId = slotId;
                    slotId++;
                    if (item.IsLongOrDouble()) slotId++;
                }
            }
            clazz.InstanceSlotCount = slotId;
        }

        private void calcInstanceFieldSlotIds(Class clazz)
        {
            int slotId = 0;
            if (clazz.SuperClass != null)
            {
                slotId = clazz.SuperClass.InstanceSlotCount;
            }

            foreach (var item in clazz.Fields)
            {
                if (!item.IsStatic)
                {
                    item.SlotId = slotId;
                    slotId++;
                    if (item.IsLongOrDouble()) slotId++;
                }
            }
            clazz.InstanceSlotCount = slotId;
        }

        private void verify(Class clazz)
        {
             
        }

        private void readClass(string name, out Stream data, out classpath.Entry entry)
        {
            classpath.ReadResult res = cp.ReadClass(name);
            if(res.err != string.Empty)
            {
                throw new System.EntryPointNotFoundException();
            }
            data = res.stream;
            entry = res.ent;
            return;
        }
    }
}