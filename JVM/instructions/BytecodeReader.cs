using System;

namespace JDRE.JVM.instructions
{
    public class BytecodeReader
    {
        public byte[] Code { get; private set; }
        public int PC { get; private set; }

        public void Reset(byte[] code, int pc)
        {
            Code = code;
            PC = pc;
        }

        public byte ReadUInt8()
        {
            byte val = Code[PC];
            PC++;
            return val;
        }

        public sbyte ReadInt8()
        {
            sbyte val = (sbyte)Code[PC];
            PC++;
            return val;
        }

        public UInt16 ReadUInt16()
        {
            int val1 = ReadUInt8();
            int val2 = ReadUInt8();
            return (UInt16)((val1 << 8) | val2);
        }

        public Int16 ReadInt16()
        {
            return (Int16)ReadUInt16();
        }

        public Int32 ReadInt32()
        {
            int val1 = ReadUInt8();
            int val2 = ReadUInt8();
            int val3 = ReadUInt8();
            int val4 = ReadUInt8();

            return (val1 << 24) | (val2 << 16) | (val3 << 8) | val4;
        }

        /// <summary>
        /// 对于跳转指令的补码padding进行跳过，保证是4的倍数
        /// </summary>
        public void SkipPadding()
        {
            while(PC % 4 !=0)
            {
                ReadUInt8();
            }
        }

        public Int32[] ReadInt32s(int count)
        {
            Int32[] result = new Int32[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = ReadInt32();
            }

            return result;
        }
    }
}