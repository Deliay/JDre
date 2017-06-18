using JDRE.JVM.exception.Lang;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.classfile
{
    class ClassFile
    {
        private UInt32 magic;
        private UInt16 majorVersion;
        private UInt16 minorVersion;
        private ConstantPool constantPool;
        private UInt16 accessFlag;
        private UInt16 thisClass;
        private UInt16 superClass;
        private UInt16[] interfaces;
        private MemberInfo[] fields;
        private MemberInfo[] methods;
        private AttributeInfo[] attributes;

        private Stream stream;
        private BinaryReader reader;
        public ClassFile(Stream stream)
        {
            this.stream = stream;
            if(stream == null)
            {
                throw new ArgumentNullException();
            }
            reader = new BinaryReader(stream);
            stream.Seek(0, SeekOrigin.Begin);
        }

        public UInt16 MinorVersion { get => minorVersion; }
        public UInt16 MajorVersion { get => majorVersion; }
        public ConstantPool Constats { get => constantPool; }
        public UInt16 AccessFlags { get => accessFlag; }
        public IReadOnlyList<MemberInfo> Fields { get => fields; }
        public IReadOnlyList<MemberInfo> Methods { get => methods; }
        public string ClassName { get => constantPool.getClassName(thisClass); }
        public string SuperClassName { get => superClass > 0 ? constantPool.getClassName(superClass) : string.Empty; }
        public IReadOnlyList<string> InterfaceNames()
        {
            List<string> list = new List<string>();
            foreach (var item in interfaces)
            {
                list.Add(constantPool.getClassName(item));
            }
            return list;
        }

        public void Read()
        {
            //magic
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            magic = reader.ReadUInt32BE();
            if (magic != 0xCAFEBABE) throw new ClassFormatError("magic error");

            //version
            minorVersion = reader.ReadUInt16BE();
            majorVersion = reader.ReadUInt16BE();

            switch (majorVersion)
            {
                case 45:
                    break;
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                    break;
                default:
                    throw new UnsupportedClassVersionError(); ;
            }

            //constant pool
            constantPool = new ConstantPool(reader);
            //access flag
            accessFlag = reader.ReadUInt16BE();
            //this class
            thisClass = reader.ReadUInt16BE();
            //super class
            superClass = reader.ReadUInt16BE();
            //interfaces
            interfaces = reader.ReadUInt16Array();
            //fields
            fields = MemberInfo.ReadMembers(reader, constantPool);
            //methods
            methods = MemberInfo.ReadMembers(reader, constantPool);
            //attributes
            attributes = AttributeInfo.ReadAttributes(reader, constantPool);
        }

    }
}
