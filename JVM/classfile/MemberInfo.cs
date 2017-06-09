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
            accessflag = reader.ReadUInt16();
            nameIndex = reader.ReadUInt16();
            descriptorIndex = reader.ReadUInt16();
            attributes = AttributeInfo.ReadAttributes(reader, cp);
        }

        public static MemberInfo[] ReadMembers(BinaryReader reader, ConstantPool cp)
        {
            int count = reader.ReadUInt16();
            MemberInfo[] res = new MemberInfo[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = new MemberInfo(reader, cp);
            }
            return res;
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