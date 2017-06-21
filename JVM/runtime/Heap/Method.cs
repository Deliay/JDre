using JDRE.JVM.classfile;
using System.IO;
using System;

namespace JDRE.JVM.runtime.Heap
{
    class Method : ClassMember
    {
        int maxStack;
        int maxLocals;
        byte[] code;
        int argSlotCount;

        public bool IsBridge { get => (AccessFlag & (int)Heap.AccessFlag.ACC_BRIDGE) != 0; }
        public bool IsVarargs { get => (AccessFlag & (int)Heap.AccessFlag.ACC_VARARGS) != 0; }
        public bool IsNative { get => (AccessFlag & (int)Heap.AccessFlag.ACC_NATIVE) != 0; }
        public bool IsAbstract { get => (AccessFlag & (int)Heap.AccessFlag.ACC_ABSTRACT) != 0; }
        public bool IsStrict { get => (AccessFlag & (int)Heap.AccessFlag.ACC_STRICT) != 0; }
        public bool IsSynchronized { get => (AccessFlag & (int)Heap.AccessFlag.ACC_SYNCHRONIZED) != 0; }

        public int MaxStack { get => maxStack; }
        public int MaxLocals { get => maxLocals; }
        public int ArgSlotCount { get => argSlotCount; }

        public byte[] Code { get => code; }

        public static Method[] CreateMethods(Class clazz, MemberInfo[] cfMethods)
        {
            Method[] methods = new Method[cfMethods.Length];
            for (int i = 0; i < cfMethods.Length; i++)
            {
                methods[i] = new Method(cfMethods[i]) { Clazz = clazz };
            }
            return methods;
        }

        public Method(MemberInfo info) : base(info)
        {
            CodeAttribute attr = info.CodeAttr();
            if (attr != null)
            {
                maxStack = attr.maxStack;
                maxLocals = attr.maxLocals;
                code = attr.code;
            }
            calcArgSlotCount();
        }

        public class MethodDescriptor
        {
            private System.Collections.Generic.List<string> parameterTypes;
            string raw = string.Empty;
            int offset = 0;

            public string[] ParamTypes { get; private set; }
            public string ReturnType { get; private set; }

            public MethodDescriptor(string strMethodDescriptor)
            {
                raw = strMethodDescriptor;
                parameterTypes = new System.Collections.Generic.List<string>(raw.Length);

                if (readChar() != '(') raiseError();
                while(true)
                {
                    string result = readFields();
                    if (result.Length > 0) parameterTypes.Add(result);
                    else break;
                }
                if (readChar() != ')') raiseError();
                if (readChar() == 'V') ReturnType = "V";
                else
                {
                    undoRead();
                    string t = readFields();
                    if (t.Length > 0) ReturnType = t;
                    else raiseError();
                }
                if (offset != raw.Length) raiseError();

                ParamTypes = parameterTypes.ToArray();
            }

            private string readFields()
            {
                char v = readChar();
                switch (v)
                {
                    case 'B': return "B";
                    case 'C': return "C";
                    case 'D': return "D";
                    case 'F':return "F";
                    case 'I': return "I";
                    case 'J': return "J";
                    case 'S': return "S";
                    case 'Z': return "Z";
                    case 'L': return readObject();
                    case '[': return readArray();
                    default:
                        undoRead();
                        return string.Empty;
                }
            }

            private string readArray()
            {
                int arrStart = offset - 1;
                readFields();
                int arrEnd = offset;
                return raw.Substring(arrStart, arrEnd - arrStart);
            }
 
            private string readObject()
            {
                string unread = raw.Substring(offset);
                int semi = unread.IndexOf(';');
                if (semi == -1) raiseError();
                int objStart = offset - 1;
                int objEnd = offset + semi + 1;
                offset = objEnd;
                return raw.Substring(objStart, objEnd - objStart - 1);
            }

            private char readChar()
            {
                return raw[offset++];
            }

            private void undoRead()
            {
                offset--;
            }

            private void raiseError()
            {
                throw new Exception(string.Format("Method desciptor parse error! ({0})", raw));
            }
        }

        private void calcArgSlotCount()
        {
            MethodDescriptor desc = new MethodDescriptor(Descriptor);
            foreach (var item in desc.ParamTypes)
            {
                argSlotCount++;
                if (item == "J" || item == "D") argSlotCount++;
            }

            if (!IsStatic) argSlotCount++;
        }
    }


}