using JDRE.JVM.classpath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.classfile
{
    [Obsolete("Single 'ClassFile' class was complete supported read from stream", true)]
    class ClassReader
    {
        BinaryReader br;

        public ClassReader(ReadResult result)
        {
            br = new BinaryReader(result.stream);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
        }

        byte readByte()
        {
            return br.ReadByte();
        }

        UInt16 readUINT16()
        {
            return br.ReadUInt16();
        }

        UInt32 readUINT32()
        {
            return br.ReadUInt32();
        }

        UInt64 readUINT64()
        {
            return br.ReadUInt64();
        }

        byte[] readBytes(Int32 count)
        {
            return br.ReadBytes(count);
        }
    }
}
