using System;
using System.IO;

namespace JDRE.JVM.classfile
{
    class AttributeInfo
    {
        protected AttributeInfo(BinaryReader reader)
        {

        }

        public static AttributeInfo CreateNextAttribute(BinaryReader reader, ConstantPool cp)
        {
            throw new NotImplementedException();
        }

        public static AttributeInfo[] ReadAttributes(BinaryReader reader, ConstantPool cp)
        {
            int count = reader.ReadUInt16();
            AttributeInfo[] res = new AttributeInfo[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = CreateNextAttribute(reader, cp);
            }
            return res;
        }

    }

    class UnparsedAttribute : AttributeInfo
    {
        public readonly string name;
        public readonly UInt32 len;
        public readonly byte[] info;

        public UnparsedAttribute(BinaryReader reader, string name, UInt32 len) : base(reader)
        {
            this.name = name;
            this.len = len;
            info = reader.ReadBytes((int)len);
        }
    }
}