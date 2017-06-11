using System;
using System.Collections.Generic;
using System.IO;

namespace JDRE.JVM.classfile
{
    enum CONSTANTS
    {
        Class = 7,
        Fieldref = 9,
        Methodref = 10,
        InterfaceMethodref = 11,
        String =8,
        Integer = 3,
        Float = 4,
        Long = 5,
        Double = 6,
        NameAndType = 12,
        Utf8 = 1,
        MethodHandle = 15,
        MethodType = 16,
        InvokeDynmaic = 18,
    }

    static class ConstantBinaryReaderHelper
    {
        public static byte[] Reverse(this byte[] b)
        {
            Array.Reverse(b);
            return b;
        }

        public static UInt16 ReadUInt16BE(this BinaryReader binRdr)
        {
            return BitConverter.ToUInt16(binRdr.ReadBytesRequired(sizeof(UInt16)).Reverse(), 0);
        }

        public static Int16 ReadInt16BE(this BinaryReader binRdr)
        {
            return BitConverter.ToInt16(binRdr.ReadBytesRequired(sizeof(Int16)).Reverse(), 0);
        }

        public static UInt32 ReadUInt32BE(this BinaryReader binRdr)
        {
            return BitConverter.ToUInt32(binRdr.ReadBytesRequired(sizeof(UInt32)).Reverse(), 0);
        }

        public static Int32 ReadInt32BE(this BinaryReader binRdr)
        {
            return BitConverter.ToInt32(binRdr.ReadBytesRequired(sizeof(Int32)).Reverse(), 0);
        }

        public static byte[] ReadBytesRequired(this BinaryReader binRdr, int byteCount)
        {
            var result = binRdr.ReadBytes(byteCount);

            if (result.Length != byteCount)
                throw new EndOfStreamException(string.Format("{0} bytes required from stream, but only {1} returned.", byteCount, result.Length));

            return result;
        }

        private static string DecodeMUTF8(byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
        public static string ReadMUTF8String(this BinaryReader reader)
        {
            int len = reader.ReadUInt16BE();
            byte[] bytes = reader.ReadBytes(len);
            return DecodeMUTF8(bytes);
        }

        public static UInt16[] ReadUInt16Array(this BinaryReader reader)
        {
            int count = reader.ReadUInt16BE();
            UInt16[] result = new UInt16[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = reader.ReadUInt16BE();
            }
            return result;
        }
    }

    class ConstantInfo
    {
        protected ConstantInfo(BinaryReader reader)
        {
            
        }

        public static ConstantInfo CreateByTag(BinaryReader reader, CONSTANTS tag, ConstantPool cp)
        {
            switch (tag)
            {
                case CONSTANTS.Class:
                    return new ConstantClassInfo(reader, cp);
                case CONSTANTS.Fieldref:
                    return new ConstantFieldrefInfo(reader, cp);
                case CONSTANTS.Methodref:
                    return new ConstantMethodrefInfo(reader, cp);
                case CONSTANTS.InterfaceMethodref:
                    return new ConstantInterfaceMethodrefInfo(reader, cp);
                case CONSTANTS.String:
                    return new ConstantStringInfo(reader, cp);
                case CONSTANTS.Integer:
                    return new ConstantIntegerInfo(reader);
                case CONSTANTS.Float:
                    return new ConstantFloatInfo(reader);
                case CONSTANTS.Long:
                    return new ConstantLongInfo(reader);
                case CONSTANTS.Double:
                    return new ConstantDoubleInfo(reader);
                case CONSTANTS.NameAndType:
                    return new ConstantNameAndTypeInfo(reader);
                case CONSTANTS.Utf8:
                    return new ConstantUTF8Info(reader);
                //case CONSTANTS.MethodHandle:
                //    return new ConstantMethodHandleInfo(reader);
                //case CONSTANTS.MethodType:
                //    return new ConstantMethodTypeInfo(reader);
                //case CONSTANTS.InvokeDynmaic:
                //    return new ConstantInfo(reader);
                default:
                    throw new ArgumentException("unknow tag");
            }
        }

        public static ConstantInfo ReadInfo(BinaryReader reader, ConstantPool cp)
        {
            CONSTANTS tag = (CONSTANTS)reader.ReadByte();
            return CreateByTag(reader, tag, cp);
        }
    }

    class ConstantIntegerInfo : ConstantInfo
    {
        public readonly Int32 value;
        public ConstantIntegerInfo(BinaryReader reader) : base(reader)
        {
            value = reader.ReadInt32BE();
        }
    }

    class ConstantFloatInfo : ConstantInfo
    {
        public readonly float value;

        public ConstantFloatInfo(BinaryReader reader) : base(reader)
        {
            value = reader.ReadSingle();
        }
    }

    class ConstantLongInfo : ConstantInfo
    {
        public readonly long value;

        public ConstantLongInfo(BinaryReader reader) : base(reader)
        {
            value = reader.ReadInt64();
        }
    }

    class ConstantDoubleInfo : ConstantInfo
    {
        public readonly double value;
        public ConstantDoubleInfo(BinaryReader reader) : base(reader)
        {
            value = reader.ReadDouble();
        }
    }

    class ConstantUTF8Info : ConstantInfo
    {
        public readonly string value;

        public ConstantUTF8Info(BinaryReader reader) : base(reader)
        {
            value = reader.ReadMUTF8String();
            //value = reader.ReadString();
        }
    }

    class ConstantStringInfo : ConstantInfo
    {
        ConstantPool cp;
        UInt16 stringIndex;
        string value;
        public ConstantStringInfo(BinaryReader reader, ConstantPool cp) : base(reader)
        {
            stringIndex = reader.ReadUInt16BE();
            this.cp = cp;
        }

        public override string ToString()
        {
            return cp.getUtf8(stringIndex);
        }
    }

    class ConstantClassInfo : ConstantInfo
    {
        ConstantPool cp;
        public readonly UInt16 nameIndex;
        public ConstantClassInfo(BinaryReader reader, ConstantPool cp) : base(reader)
        {
            this.cp = cp;
            nameIndex = reader.ReadUInt16BE();
        }

        public override string ToString()
        {
            return cp.getUtf8(nameIndex);
        }
    }

    class ConstantNameAndTypeInfo : ConstantInfo
    {
        public readonly UInt16 nameIndex;
        public readonly UInt16 descriptorIndex;

        public ConstantNameAndTypeInfo(BinaryReader reader) : base(reader)
        {
            nameIndex = reader.ReadUInt16BE();
            descriptorIndex = reader.ReadUInt16BE();
        }
    }

    class ConstantMemberrefInfo : ConstantInfo
    {
        UInt16 classIndex;
        UInt16 nameAndTypeIndex;
        ConstantPool cp;
        public ConstantMemberrefInfo(BinaryReader reader, ConstantPool cp) : base(reader)
        {
            this.cp = cp;
            classIndex = reader.ReadUInt16BE();
            nameAndTypeIndex = reader.ReadUInt16BE();
        }

        public string ClassName()
        {
            return cp.getClassName(classIndex);
        }

        public string[] NameAndDescriptor()
        {
            return cp.getNameAndType(nameAndTypeIndex);
        }
    }

    class ConstantFieldrefInfo : ConstantMemberrefInfo
    {
        public ConstantFieldrefInfo(BinaryReader reader, ConstantPool cp) : base(reader, cp)
        {
        }
    }

    class ConstantMethodrefInfo : ConstantMemberrefInfo
    {
        public ConstantMethodrefInfo(BinaryReader reader, ConstantPool cp) : base(reader, cp)
        {
        }
    }

    class ConstantInterfaceMethodrefInfo : ConstantMemberrefInfo
    {
        public ConstantInterfaceMethodrefInfo(BinaryReader reader, ConstantPool cp) : base(reader, cp)
        {
        }
    }

    class ConstantPool
    {
        ConstantInfo[] cp;
        BinaryReader reader;
        int count;
        public int Count { get => count; }
        public ConstantPool(BinaryReader reader)
        {
            this.reader = reader;
            count = reader.ReadUInt16BE();
            cp = new ConstantInfo[count];

            for (int i = 1; i < count; i++)
            {
                cp[i] = ConstantInfo.ReadInfo(reader, this);
                if(cp[i] is ConstantLongInfo || cp[i] is ConstantDoubleInfo)
                {
                    i++;
                }
            }
        }

        public ConstantInfo getConstantInfo(UInt16 index)
        {
            if (index < cp.Length && cp[index] != null) return cp[index];
            else throw new NullReferenceException("Can't find constant in Index:" + index);
        }

        public string getClassName(UInt16 index)
        {
            ConstantClassInfo info = getConstantInfo(index) as ConstantClassInfo;
            return getUtf8(info.nameIndex);
        }

        public string[] getNameAndType(UInt16 index)
        {
            ConstantNameAndTypeInfo info = getConstantInfo(index) as ConstantNameAndTypeInfo;
            return new[] { getUtf8(info.nameIndex), getUtf8(info.descriptorIndex) };
        }

        public string getUtf8(UInt16 index)
        {
            ConstantUTF8Info info = getConstantInfo(index) as ConstantUTF8Info;
            return info.value;
        }
    }
}