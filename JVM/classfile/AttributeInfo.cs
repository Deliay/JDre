using System;
using System.IO;

namespace JDRE.JVM.classfile
{
    class AttributeInfo
    {
        protected AttributeInfo(BinaryReader reader)
        {

        }

        public static AttributeInfo CreateAttribute(BinaryReader reader, ConstantPool cp)
        {
            UInt16 attrNameIndex = reader.ReadUInt16BE();
            string attrName = cp.getUtf8(attrNameIndex);
            UInt32 attrLen = reader.ReadUInt32BE();
            AttributeInfo attrInfo;
            switch (attrName)
            {
                case "Code":
                    attrInfo = new CodeAttribute(reader, cp);
                    break;
                case "ConstantValue":
                    attrInfo = new ConstantValueAttribute(reader);
                    break;
                case "Deprecated":
                    attrInfo = new DeprecatedAttribute(reader);
                    break;
                case "Exception":
                    attrInfo = new ExceptionsAttribute(reader);
                    break;
                case "LineNumberTable":
                    attrInfo = new LineNumberTableAttribute(reader);
                    break;
                case "LocalVariableTable":
                    attrInfo = new LocalVariableTableAttribute(reader);
                    break;
                case "SourceFile":
                    attrInfo = new SourceFileAttribute(reader, cp);
                    break;
                case "Synthetic":
                    attrInfo = new SyntheticAttribute(reader);
                    break;
                default:
                    attrInfo = new UnparsedAttribute(reader, attrName, attrLen);
                    break;
            }

            return attrInfo;
        }

        public static AttributeInfo[] ReadAttributes(BinaryReader reader, ConstantPool cp)
        {
            int count = reader.ReadUInt16BE();
            AttributeInfo[] res = new AttributeInfo[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = CreateAttribute(reader, cp);
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

    class MarkerAttribute : AttributeInfo
    {
        public MarkerAttribute(BinaryReader reader) : base(reader)
        {
        }
    }

    class DeprecatedAttribute : MarkerAttribute
    {
        public DeprecatedAttribute(BinaryReader reader) : base(reader)
        {
        }
    }

    class SyntheticAttribute : MarkerAttribute
    {
        public SyntheticAttribute(BinaryReader reader) : base(reader)
        {
        }
    }

    class SourceFileAttribute : AttributeInfo
    {
        UInt16 index;
        ConstantPool cp;
        public SourceFileAttribute(BinaryReader reader, ConstantPool cp) : base(reader)
        {
            index = reader.ReadUInt16BE();
            this.cp = cp;
        }

        public string FileName { get => cp.getUtf8(index); }
        public UInt16 Index { get => index; }
    }

    class ConstantValueAttribute : AttributeInfo
    {
        UInt16 index;

        public ConstantValueAttribute(BinaryReader reader) : base(reader)
        {
            index = reader.ReadUInt16BE();
        }

        public UInt16 Index { get => index; }
    }

    struct ExceptionTableEntry
    {
        public UInt16 startPc, endPc, handlerPc, catchType;
    }

    class CodeAttribute : AttributeInfo
    {
        public readonly ConstantPool cp;
        public readonly UInt16 maxStack, maxLocals;
        public readonly uint codelen;
        public readonly byte[] code;
        public readonly ExceptionTableEntry[] exceptionTable;
        public readonly AttributeInfo[] attributes;

        public CodeAttribute(BinaryReader reader, ConstantPool cp) : base(reader)
        {
            this.cp = cp;
            maxStack = reader.ReadUInt16BE();
            maxLocals = reader.ReadUInt16BE();
            codelen = reader.ReadUInt32BE();
            code = reader.ReadBytes((int)codelen);
            int len = reader.ReadUInt16BE();
            exceptionTable = new ExceptionTableEntry[len];
            for (int i = 0; i < len; i++)
            {
                exceptionTable[i] = new ExceptionTableEntry() {
                    startPc = reader.ReadUInt16BE(),
                    endPc = reader.ReadUInt16BE(),
                    handlerPc = reader.ReadUInt16BE(),
                    catchType = reader.ReadUInt16BE()
                };
            }

            attributes = ReadAttributes(reader, cp);

        }


    }

    class ExceptionsAttribute : AttributeInfo
    {
        UInt16[] indexTable;

        public ExceptionsAttribute(BinaryReader reader) : base(reader)
        {
            indexTable = reader.ReadUInt16Array();
        }

        public UInt16[] IndexTable { get => indexTable; }
    }

    struct LineNumberTableEntry
    {
        public UInt16 startPc, lineNumber;
    }

    class LineNumberTableAttribute : AttributeInfo
    {
        LineNumberTableEntry[] table;
        public LineNumberTableAttribute(BinaryReader reader) : base(reader)
        {
            int len = reader.ReadUInt16BE();
            table = new LineNumberTableEntry[len];
            for (int i = 0; i < len; i++)
            {
                table[i] = new LineNumberTableEntry() { startPc = reader.ReadUInt16BE(), lineNumber = reader.ReadUInt16BE() };
            }
        }
    }

    struct LocalVariableTableEntry
    {
        public UInt16 startPc, length;
    }

    class LocalVariableTableAttribute : AttributeInfo
    {
        LocalVariableTableEntry[] table;
        public LocalVariableTableAttribute(BinaryReader reader) : base(reader)
        {
            int len = reader.ReadUInt16BE();
            table = new LocalVariableTableEntry[len];
            for (int i = 0; i < len; i++)
            {
                table[i] = new LocalVariableTableEntry() { startPc = reader.ReadUInt16BE(), length = reader.ReadUInt16BE() };
            }
        }
    }
}