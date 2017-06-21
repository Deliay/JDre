using JDRE.JVM.instructions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDRE.JVM.runtime;
using JDRE.JVM.runtime.Heap;
using JDRE.JVM.exception.Lang;

namespace JDRE.JVM.instructions.References
{
    // New 指令
    // 不含数组
    class NEW : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            //通过字节码附带的索引，从常量池中找到类符号引用，拿到类数据
            ConstantPool cp = frame.Method.Clazz.HeapConstants;
            ClassReference clzref = cp.GetConstant(Index) as ClassReference;
            Class clazz = clzref.ResolvedClass();

            //不能创建接口和抽象类的实例
            if (clazz.IsInterface || clazz.IsAbstract) throw new InstantiationError();

            //创建运行时对象，并将对象推入栈顶
            runtime.Object refer = clazz.ToObject();
            frame.OperandStack.PushObject(refer);
        }
    }
    
    //给静态变量赋值
    class PUT_STATIC : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            Method curMethod = frame.Method;
            Class curClazz = frame.Method.Clazz;

            ConstantPool curCp = curClazz.HeapConstants;

            FieldReference fieldRef = curCp.GetConstant(Index) as FieldReference;
            Field field = fieldRef.ResolveField();
            Class fieldClass = field.Clazz;

            if (!field.IsStatic) throw new IncompatiableClassChangeError();

            if (field.IsFinal)
                if (curClazz != fieldClass || curMethod.Name != "<clinit>") throw new IllegalAccessError();

            string descriptor = field.Descriptor;
            int slotId = field.SlotId;
            Slots slots = fieldClass.StaticVars;
            OperandStack stack = frame.OperandStack;

            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    slots.SetInt32(slotId, stack.PopInt32());
                    break;
                case 'F':
                    slots.SetFloat(slotId, stack.PopFloat());
                    break;
                case 'J':
                    slots.SetLong(slotId, stack.PopLong());
                    break;
                case 'D':
                    slots.SetDouble(slotId, stack.PopDouble());
                    break;
                case 'L':
                    slots.SetRefer(slotId, stack.PopObject());
                    break;
                default:
                    break;
            }

        }
    }

    //取静态变量的值
    class GET_STATIC : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            ConstantPool cp = frame.Method.Clazz.HeapConstants;
            FieldReference fieldRef = cp.GetConstant(Index) as FieldReference;
            Field field = fieldRef.ResolveField();
            Class clazz = field.Clazz;

            if (!field.IsStatic) throw new FieldAccessException();


            string descriptor = field.Descriptor;
            int slotId = field.SlotId;
            Slots slots = clazz.StaticVars;
            OperandStack stack = frame.OperandStack;

            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    stack.PushInt32(slots.GetInt32(slotId));
                    break;
                case 'F':
                    stack.PushFloat(slots.GetFloat(slotId));
                    break;
                case 'J':
                    stack.PushLong(slots.GetLong(slotId));
                    break;
                case 'D':
                    stack.PushDouble(slots.GetDouble(slotId));
                    break;
                case 'L':
                    stack.PushObject(slots.GetRefer(slotId));
                    break;
                default:
                    break;
            }

        }
    }

    //给成员变量赋值
    class PUT_FIELD : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            Method curMethod = frame.Method;
            Class curClazz = frame.Method.Clazz;

            ConstantPool curCp = curClazz.HeapConstants;

            FieldReference fieldRef = curCp.GetConstant(Index) as FieldReference;
            Field field = fieldRef.ResolveField();

            if (field.IsStatic) throw new IncompatiableClassChangeError();

            if (field.IsFinal)
                if (curClazz != field.Clazz || curMethod.Name != "<init>") throw new IllegalAccessError();

            string descriptor = field.Descriptor;
            int slotId = field.SlotId;
            OperandStack stack = frame.OperandStack;
            runtime.Object refer = null;

            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    int ival = stack.PopInt32();
                    refer = stack.PopObject();
                    if (refer == null) throw new NullPointerException();
                    refer.Fields.SetInt32(slotId, ival);
                    break;
                case 'F':
                    float fval = stack.PopFloat();
                    refer = stack.PopObject();
                    if (refer == null) throw new NullPointerException();
                    refer.Fields.SetFloat(slotId, fval);
                    break;
                case 'J':
                    long lval = stack.PopLong();
                    refer = stack.PopObject();
                    if (refer == null) throw new NullPointerException();
                    refer.Fields.SetLong(slotId, lval);
                    break;
                case 'D':
                    double dval = stack.PopDouble();
                    refer = stack.PopObject();
                    if (refer == null) throw new NullPointerException();
                    refer.Fields.SetDouble(slotId, dval);
                    break;
                case 'L':
                    runtime.Object oval = stack.PopObject();
                    refer = stack.PopObject();
                    if (refer == null) throw new NullReferenceException();
                    refer.Fields.SetRefer(slotId, oval);
                    break;
                default:
                    break;
            }

        }
    }

    //获取成员变量的值
    class GET_FIELD : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            ConstantPool cp = frame.Method.Clazz.HeapConstants;
            FieldReference fref = cp.GetConstant(Index) as FieldReference;
            Field f = fref.ResolveField();

            if (f.IsStatic) throw new IncompatiableClassChangeError();

            OperandStack stack = frame.OperandStack;
            runtime.Object obj = stack.PopObject();

            if (obj == null) throw new NullPointerException();

            string descriptor = f.Descriptor;
            int slotId = f.SlotId;
            Slots slots = obj.Fields;

            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    stack.PushInt32(slots.GetInt32(slotId));
                    break;
                case 'F':
                    stack.PushFloat(slots.GetFloat(slotId));
                    break;
                case 'J':
                    stack.PushLong(slots.GetLong(slotId));
                    break;
                case 'D':
                    stack.PushDouble(slots.GetDouble(slotId));
                    break;
                case 'L':
                    stack.PushObject(slots.GetRefer(slotId));
                    break;
                default:
                    break;
            }
        }
    }

    //判断是否是某个类的实例
    class INSTANCE_OF : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            OperandStack stack = frame.OperandStack;
            runtime.Object obj = stack.PopObject();

            if(obj == null)
            {
                stack.PushInt32(0);
                return;
            }

            ConstantPool cp = frame.Method.Clazz.HeapConstants;
            ClassReference crf = cp.GetConstant(Index) as ClassReference;
            Class clz = crf.ResolvedClass();

            if (obj.IsInstanceOf(clz)) stack.PushInt32(1);
            else stack.PushInt32(0);
        }
    }

    class CHECK_CAST : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            OperandStack stack = frame.OperandStack;
            runtime.Object obj = stack.PopObject();
            stack.PushObject(obj);

            if (obj == null)
            {
                return;
            }

            ConstantPool cp = frame.Method.Clazz.HeapConstants;
            ClassReference crf = cp.GetConstant(Index) as ClassReference;
            Class clz = crf.ResolvedClass();

            if (!obj.IsInstanceOf(clz)) 
            {
                throw new ClassCastException();
            }

        }
    }

    //InvokeSpecial 指令
    //[Obsolete("暂时hack")]
    //class INVOKE_SPECIAL : Index16Instruction
    //{
    //    public override void Execute(Frame frame)
    //    {
    //        frame.OperandStack.PopObject();
    //    }
    //}

    class INVOKE_STATIC : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            ConstantPool cp = frame.Method.Clazz.HeapConstants;
            MethodReference mref = cp.GetConstant(Index) as MethodReference;
            Method resolvedMethod = mref.ResolvedMethod();
            if (!resolvedMethod.IsStatic) throw new IncompatiableClassChangeError();
            InvokerHelper.InvokeMethod(frame, resolvedMethod);
        }
    }
    class INVOKE_SPECIAL : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            Class clz = frame.Method.Clazz;
            ConstantPool cp = clz.HeapConstants;
            MethodReference mref = cp.GetConstant(Index) as MethodReference;
            Class resolvedClass = mref.ResolvedClass();
            Method resolvedMethod = mref.ResolvedMethod();

            if (resolvedMethod.Name == "<init>" && resolvedMethod.Clazz != resolvedClass) throw new NoSuchMethodError();
            if (resolvedMethod.IsStatic) throw new IncompatiableClassChangeError();

            runtime.Object obj = frame.OperandStack.GetObjectFromTop(resolvedMethod.ArgSlotCount - 1);

            if (obj == null) throw new NullPointerException();

            //确保protected方法只能被其子类或本类的方法调用
            if (resolvedMethod.IsProtected &&
                resolvedMethod.Clazz.IsSuperClassOf(clz) &&
                resolvedMethod.Clazz.getPackageName() != clz.getPackageName() &&
                obj.Clazz != clz &&
                !obj.Clazz.IsSubClassOf(clz)) throw new IllegalAccessError();

            Method callee = resolvedMethod;

            if(clz.IsSuper && 
                resolvedClass.IsSuperClassOf(clz) && 
                resolvedMethod.Name != "<init>")
            {
                callee = MethodReference.LookupMethodInClass(clz.SuperClass, mref.Name, mref.Descriptor);
            }

            if (callee == null || callee.IsAbstract) throw new AbstractMethodError();

            InvokerHelper.InvokeMethod(frame, callee);
        }
    }

    //[Obsolete("hack")]
    //class INVOKE_VIRTUAL : Index16Instruction
    //{
    //    public override void Execute(Frame frame)
    //    {
    //        ConstantPool cp = frame.Method.Clazz.HeapConstants;
    //        MethodReference mref = cp.GetConstant(Index) as MethodReference;

    //        if(mref.Name == "println")
    //        {
    //            OperandStack stack = frame.OperandStack;
    //            switch (mref.Descriptor)
    //            {
    //                case "(Z)V": Console.WriteLine(stack.PopInt32() !=0 ); break;
    //                case "(C)V": Console.WriteLine(stack.PopInt32()); break;
    //                case "(B)V": Console.WriteLine(stack.PopInt32()); break;
    //                case "(S)V": Console.WriteLine(stack.PopInt32()); break;
    //                case "(I)V": Console.WriteLine(stack.PopInt32()); break;
    //                case "(J)V": Console.WriteLine(stack.PopLong()); break;
    //                case "(F)V": Console.WriteLine(stack.PopFloat()); break;
    //                case "(D)V": Console.WriteLine(stack.PopDouble()); break;
    //                default:
    //                    Console.WriteLine("OUT: " + mref.Descriptor);
    //                    break;
    //            }
    //            stack.PopObject();
    //        }
    //    }
    //}

    class INVOKE_VIRTUAL : Index16Instruction
    {
        public override void Execute(Frame frame)
        {
            Class clz = frame.Method.Clazz;
            ConstantPool cp = clz.HeapConstants;
            MethodReference mref = cp.GetConstant(Index) as MethodReference;
            Class resolvedClass = mref.ResolvedClass();
            Method resolvedMethod = mref.ResolvedMethod();
            
            if (resolvedMethod.IsStatic) throw new IncompatiableClassChangeError();

            runtime.Object obj = frame.OperandStack.GetObjectFromTop(resolvedMethod.ArgSlotCount - 1);

            if (obj == null)
            {
                // native hack
                if(mref.Name == "println")
                {
                    InvokerHelper.println(frame.OperandStack, mref.Descriptor);
                }
                throw new NullPointerException();
            }
            //确保protected方法只能被其子类或本类的方法调用
            if (resolvedMethod.IsProtected &&
                resolvedMethod.Clazz.IsSuperClassOf(clz) &&
                resolvedMethod.Clazz.getPackageName() != clz.getPackageName() &&
                obj.Clazz != clz &&
                !obj.Clazz.IsSubClassOf(clz)) throw new IllegalAccessError();

            Method callee = MethodReference.LookupMethodInClass(obj.Clazz, mref.Name, mref.Descriptor);
            if (callee == null || callee.IsAbstract) throw new AbstractMethodError();

            InvokerHelper.InvokeMethod(frame, callee);
        }
    }
}
