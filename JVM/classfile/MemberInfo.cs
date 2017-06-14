using System;
using System.Collections.Generic;
using System.IO;

namespace JDRE.JVM.classfile
{
    class MemberInfo
    {
        ConstantPool cp;
        public readonly UInt16 accessflag;
        public readonly UInt16 nameIndex;
        public readonly UInt16 descriptorIndex;
        public readonly AttributeInfo[] attributes;

        private MemberInfo(BinaryReader reader, ConstantPool cp)
        {
            this.cp = cp;
            accessflag = reader.ReadUInt16BE();
            nameIndex = reader.ReadUInt16BE();
            descriptorIndex = reader.ReadUInt16BE();
            attributes = AttributeInfo.ReadAttributes(reader, cp);
        }

        public static MemberInfo[] ReadMembers(BinaryReader reader, ConstantPool cp)
        {
            int count = reader.ReadUInt16BE();
            MemberInfo[] res = new MemberInfo[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = new MemberInfo(reader, cp);
            }
            return res;
        }

        public CodeAttribute CodeAttr()
        {
            foreach (var item in attributes)
            {
                if (item is CodeAttribute) return (CodeAttribute)item;
            }
            return null;
        }

        public string Name()
        {
            return cp.getUtf8(nameIndex);
        }

        public string Descriptor()
        {
            return cp.getUtf8(descriptorIndex);
        }
    }
}