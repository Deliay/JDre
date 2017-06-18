using JDRE.JVM.instructions.Base;
using JDRE.JVM.instructions.Comparisions;
using JDRE.JVM.instructions.Constants;
using JDRE.JVM.instructions.Control;
using JDRE.JVM.instructions.Convertions;
using JDRE.JVM.instructions.Extended;
using JDRE.JVM.instructions.Loads;
using JDRE.JVM.instructions.Math;
using JDRE.JVM.instructions.References;
using JDRE.JVM.instructions.Stack;
using JDRE.JVM.instructions.Stores;
using JDRE.JVM.runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.instructions.Initial
{
    static class InstructionAllocatorHelper
    {
        public static NOP nop = new NOP();
        public static ACONST_NULL aconst_null = new ACONST_NULL();
        public static ICONST_M1 iconst_m1 = new ICONST_M1();
        public static ICONST_0 iconst_0 = new ICONST_0();
        public static ICONST_1 iconst_1 = new ICONST_1();
        public static ICONST_2 iconst_2 = new ICONST_2();
        public static ICONST_3 iconst_3 = new ICONST_3();
        public static ICONST_4 iconst_4 = new ICONST_4();
        public static ICONST_5 iconst_5 = new ICONST_5();
        public static LCONST_0 lconst_0 = new LCONST_0();
        public static LCONST_1 lconst_1 = new LCONST_1();
        public static FCONST_0 fconst_0 = new FCONST_0();
        public static FCONST_1 fconst_1 = new FCONST_1();
        public static FCONST_2 fconst_2 = new FCONST_2();
        public static DCONST_0 dconst_0 = new DCONST_0();
        public static DCONST_1 dconst_1 = new DCONST_1();
        public static ILOAD_0 iload_0 = new ILOAD_0();
        public static ILOAD_1 iload_1 = new ILOAD_1();
        public static ILOAD_2 iload_2 = new ILOAD_2();
        public static ILOAD_3 iload_3 = new ILOAD_3();
        public static LLOAD_0 lload_0 = new LLOAD_0();
        public static LLOAD_1 lload_1 = new LLOAD_1();
        public static LLOAD_2 lload_2 = new LLOAD_2();
        public static LLOAD_3 lload_3 = new LLOAD_3();
        public static FLOAD_0 fload_0 = new FLOAD_0();
        public static FLOAD_1 fload_1 = new FLOAD_1();
        public static FLOAD_2 fload_2 = new FLOAD_2();
        public static FLOAD_3 fload_3 = new FLOAD_3();
        public static DLOAD_0 dload_0 = new DLOAD_0();
        public static DLOAD_1 dload_1 = new DLOAD_1();
        public static DLOAD_2 dload_2 = new DLOAD_2();
        public static DLOAD_3 dload_3 = new DLOAD_3();
        public static ALOAD_0 aload_0 = new ALOAD_0();
        public static ALOAD_1 aload_1 = new ALOAD_1();
        public static ALOAD_2 aload_2 = new ALOAD_2();
        public static ALOAD_3 aload_3 = new ALOAD_3();
        //public static IALOAD iaload = new IALOAD();
        //public static LALOAD laload = new LALOAD();
        //public static FALOAD faload = new FALOAD();
        //public static DALOAD daload = new DALOAD();
        //public static AALOAD aaload = new AALOAD();
        //public static BALOAD baload = new BALOAD();
        //public static CALOAD caload = new CALOAD();
        //public static SALOAD saload = new SALOAD();
        public static ISTORE_0 istore_0 = new ISTORE_0();
        public static ISTORE_1 istore_1 = new ISTORE_1();
        public static ISTORE_2 istore_2 = new ISTORE_2();
        public static ISTORE_3 istore_3 = new ISTORE_3();
        public static LSTORE_0 lstore_0 = new LSTORE_0();
        public static LSTORE_1 lstore_1 = new LSTORE_1();
        public static LSTORE_2 lstore_2 = new LSTORE_2();
        public static LSTORE_3 lstore_3 = new LSTORE_3();
        public static FSTORE_0 fstore_0 = new FSTORE_0();
        public static FSTORE_1 fstore_1 = new FSTORE_1();
        public static FSTORE_2 fstore_2 = new FSTORE_2();
        public static FSTORE_3 fstore_3 = new FSTORE_3();
        public static DSTORE_0 dstore_0 = new DSTORE_0();
        public static DSTORE_1 dstore_1 = new DSTORE_1();
        public static DSTORE_2 dstore_2 = new DSTORE_2();
        public static DSTORE_3 dstore_3 = new DSTORE_3();
        public static ASTORE_0 astore_0 = new ASTORE_0();
        public static ASTORE_1 astore_1 = new ASTORE_1();
        public static ASTORE_2 astore_2 = new ASTORE_2();
        public static ASTORE_3 astore_3 = new ASTORE_3();
        //public static IASTORE iastore = new IASTORE();
        //public static LASTORE lastore = new LASTORE();
        //public static FASTORE fastore = new FASTORE();
        //public static DASTORE dastore = new DASTORE();
        //public static AASTORE aastore = new AASTORE();
        //public static BASTORE bastore = new BASTORE();
        //public static CASTORE castore = new CASTORE();
        //public static SASTORE sastore = new SASTORE();
        public static POP pop = new POP();
        public static POP2 pop2 = new POP2();
        public static DUP dup = new DUP();
        public static DUP_X1 dup_x1 = new DUP_X1();
        public static DUP_X2 dup_x2 = new DUP_X2();
        public static DUP2 dup2 = new DUP2();
        public static DUP2_X1 dup2_x1 = new DUP2_X1();
        public static DUP2_X2 dup2_x2 = new DUP2_X2();
        public static SWAP swap = new SWAP();
        public static IADD iadd = new IADD();
        public static LADD ladd = new LADD();
        public static FADD fadd = new FADD();
        public static DADD dadd = new DADD();
        public static ISUB isub = new ISUB();
        public static LSUB lsub = new LSUB();
        public static FSUB fsub = new FSUB();
        public static DSUB dsub = new DSUB();
        public static IMUL imul = new IMUL();
        public static LMUL lmul = new LMUL();
        public static FMUL fmul = new FMUL();
        public static DMUL dmul = new DMUL();
        public static IDIV idiv = new IDIV();
        public static LDIV ldiv = new LDIV();
        public static FDIV fdiv = new FDIV();
        public static DDIV ddiv = new DDIV();
        public static IREM irem = new IREM();
        public static LREM lrem = new LREM();
        public static FREM frem = new FREM();
        public static DREM drem = new DREM();
        public static INEG ineg = new INEG();
        public static LNEG lneg = new LNEG();
        public static FNEG fneg = new FNEG();
        public static DNEG dneg = new DNEG();
        public static ISHL ishl = new ISHL();
        public static LSHL lshl = new LSHL();
        public static ISHR ishr = new ISHR();
        public static LSHR lshr = new LSHR();
        public static IUSHR iushr = new IUSHR();
        public static LUSHR lushr = new LUSHR();
        public static IAND iand = new IAND();
        public static LAND land = new LAND();
        public static IOR ior = new IOR();
        public static LOR lor = new LOR();
        public static IXOR ixor = new IXOR();
        public static LXOR lxor = new LXOR();
        public static I2L i2l = new I2L();
        public static I2F i2f = new I2F();
        public static I2D i2d = new I2D();
        public static L2I l2i = new L2I();
        public static L2F l2f = new L2F();
        public static L2D l2d = new L2D();
        public static F2I f2i = new F2I();
        public static F2L f2l = new F2L();
        public static F2D f2d = new F2D();
        public static D2I d2i = new D2I();
        public static D2L d2l = new D2L();
        public static D2F d2f = new D2F();
        public static I2B i2b = new I2B();
        public static I2C i2c = new I2C();
        public static I2S i2s = new I2S();
        public static LCMP lcmp = new LCMP();
        public static FCMPL fcmpl = new FCMPL();
        public static FCMPG fcmpg = new FCMPG();
        public static DCMPL dcmpl = new DCMPL();
        public static DCMPG dcmpg = new DCMPG();
        //public static IRETURN ireturn = new IRETURN();
        //public static LRETURN lreturn = new LRETURN();
        //public static FRETURN freturn = new FRETURN();
        //public static DRETURN dreturn = new DRETURN();
        //public static ARETURN areturn = new ARETURN();
        //public static RETURN _return = new RETURN();
        //public static ARRAY_LENGTH arraylength = new ARRAY_LENGTH();
        //public static ATHROW athrow = new ATHROW();
        //public static MONITOR_ENTER monitorenter = new MONITOR_ENTER();
        //public static MONITOR_EXIT monitorexit = new MONITOR_EXIT();
        //public static INVOKE_NATIVE invoke_native = new INVOKE_NATIVE();

        public static Instruction CreateInstruction(byte opcode)
        {
            switch (opcode)
            {
                case 0x00:
                    return nop;
                case 0x01:
                    return aconst_null;
                case 0x02:
                    return iconst_m1;
                case 0x03:
                    return iconst_0;
                case 0x04:
                    return iconst_1;
                case 0x05:
                    return iconst_2;
                case 0x06:
                    return iconst_3;
                case 0x07:
                    return iconst_4;
                case 0x08:
                    return iconst_5;
                case 0x09:
                    return lconst_0;
                case 0x0a:
                    return lconst_1;
                case 0x0b:
                    return fconst_0;
                case 0x0c:
                    return fconst_1;
                case 0x0d:
                    return fconst_2;
                case 0x0e:
                    return dconst_0;
                case 0x0f:
                    return dconst_1;
                case 0x10:
                    return new BIPUSH();
                case 0x11:
                    return new SIPUSH();
                case 0x12:
                    return new LDC();
                 case 0x13:
                    return new LDC_W();
                 case 0x14:
                    return new LDC2_W();
                case 0x15:
                    return new ILOAD();
                case 0x16:
                    return new LLOAD();
                case 0x17:
                    return new FLOAD();
                case 0x18:
                    return new DLOAD();
                case 0x19:
                    return new ALOAD();
                case 0x1a:
                    return iload_0;
                case 0x1b:
                    return iload_1;
                case 0x1c:
                    return iload_2;
                case 0x1d:
                    return iload_3;
                case 0x1e:
                    return lload_0;
                case 0x1f:
                    return lload_1;
                case 0x20:
                    return lload_2;
                case 0x21:
                    return lload_3;
                case 0x22:
                    return fload_0;
                case 0x23:
                    return fload_1;
                case 0x24:
                    return fload_2;
                case 0x25:
                    return fload_3;
                case 0x26:
                    return dload_0;
                case 0x27:
                    return dload_1;
                case 0x28:
                    return dload_2;
                case 0x29:
                    return dload_3;
                case 0x2a:
                    return aload_0;
                case 0x2b:
                    return aload_1;
                case 0x2c:
                    return aload_2;
                case 0x2d:
                    return aload_3;
                //case 0x2e:
                //    return iaload;
                // case 0x2f:
                // 	return laload;
                // case 0x30:
                // 	return faload;
                // case 0x31:
                // 	return daload;
                // case 0x32:
                // 	return aaload;
                // case 0x33:
                // 	return baload;
                // case 0x34:
                // 	return caload;
                // case 0x35:
                // 	return saload;
                case 0x36:
                    return new ISTORE();
                case 0x37:
                    return new LSTORE();
                case 0x38:
                    return new FSTORE();
                case 0x39:
                    return new DSTORE();
                case 0x3a:
                    return new ASTORE();
                case 0x3b:
                    return istore_0;
                case 0x3c:
                    return istore_1;
                case 0x3d:
                    return istore_2;
                case 0x3e:
                    return istore_3;
                case 0x3f:
                    return lstore_0;
                case 0x40:
                    return lstore_1;
                case 0x41:
                    return lstore_2;
                case 0x42:
                    return lstore_3;
                case 0x43:
                    return fstore_0;
                case 0x44:
                    return fstore_1;
                case 0x45:
                    return fstore_2;
                case 0x46:
                    return fstore_3;
                case 0x47:
                    return dstore_0;
                case 0x48:
                    return dstore_1;
                case 0x49:
                    return dstore_2;
                case 0x4a:
                    return dstore_3;
                case 0x4b:
                    return astore_0;
                case 0x4c:
                    return astore_1;
                case 0x4d:
                    return astore_2;
                case 0x4e:
                    return astore_3;
                // case 0x4f:
                // 	return iastore
                // case 0x50:
                // 	return lastore
                // case 0x51:
                // 	return fastore
                // case 0x52:
                // 	return dastore
                // case 0x53:
                // 	return aastore
                // case 0x54:
                // 	return bastore
                // case 0x55:
                // 	return castore
                // case 0x56:
                // 	return sastore
                case 0x57:
                    return pop;
                case 0x58:
                    return pop2;
                case 0x59:
                    return dup;
                case 0x5a:
                    return dup_x1;
                case 0x5b:
                    return dup_x2;
                case 0x5c:
                    return dup2;
                case 0x5d:
                    return dup2_x1;
                case 0x5e:
                    return dup2_x2;
                case 0x5f:
                    return swap;
                case 0x60:
                    return iadd;
                case 0x61:
                    return ladd;
                case 0x62:
                    return fadd;
                case 0x63:
                    return dadd;
                case 0x64:
                    return isub;
                case 0x65:
                    return lsub;
                case 0x66:
                    return fsub;
                case 0x67:
                    return dsub;
                case 0x68:
                    return imul;
                case 0x69:
                    return lmul;
                case 0x6a:
                    return fmul;
                case 0x6b:
                    return dmul;
                case 0x6c:
                    return idiv;
                case 0x6d:
                    return ldiv;
                case 0x6e:
                    return fdiv;
                case 0x6f:
                    return ddiv;
                case 0x70:
                    return irem;
                case 0x71:
                    return lrem;
                case 0x72:
                    return frem;
                case 0x73:
                    return drem;
                case 0x74:
                    return ineg;
                case 0x75:
                    return lneg;
                case 0x76:
                    return fneg;
                case 0x77:
                    return dneg;
                case 0x78:
                    return ishl;
                case 0x79:
                    return lshl;
                case 0x7a:
                    return ishr;
                case 0x7b:
                    return lshr;
                case 0x7c:
                    return iushr;
                case 0x7d:
                    return lushr;
                case 0x7e:
                    return iand;
                case 0x7f:
                    return land;
                case 0x80:
                    return ior;
                case 0x81:
                    return lor;
                case 0x82:
                    return ixor;
                case 0x83:
                    return lxor;
                case 0x84:
                    return new IINC();
                case 0x85:
                    return i2l;
                case 0x86:
                    return i2f;
                case 0x87:
                    return i2d;
                case 0x88:
                    return l2i;
                case 0x89:
                    return l2f;
                case 0x8a:
                    return l2d;
                case 0x8b:
                    return f2i;
                case 0x8c:
                    return f2l;
                case 0x8d:
                    return f2d;
                case 0x8e:
                    return d2i;
                case 0x8f:
                    return d2l;
                case 0x90:
                    return d2f;
                case 0x91:
                    return i2b;
                case 0x92:
                    return i2c;
                case 0x93:
                    return i2s;
                case 0x94:
                    return lcmp;
                case 0x95:
                    return fcmpl;
                case 0x96:
                    return fcmpg;
                case 0x97:
                    return dcmpl;
                case 0x98:
                    return dcmpg;
                case 0x99:
                    return new IFEQ();
                case 0x9a:
                    return new IFNE();
                case 0x9b:
                    return new IFLT();
                case 0x9c:
                    return new IFGE();
                case 0x9d:
                    return new IFGT();
                case 0x9e:
                    return new IFLE();
                case 0x9f:
                    return new IF_ICMPEQ();
                case 0xa0:
                    return new IF_ICMPNE();
                case 0xa1:
                    return new IF_ICMPLT();
                case 0xa2:
                    return new IF_ICMPGE();
                case 0xa3:
                    return new IF_ICMPGT();
                case 0xa4:
                    return new IF_ICMPLE();
                case 0xa5:
                    return new IF_ACMPEQ();
                case 0xa6:
                    return new IF_ACMPNE();
                case 0xa7:
                    return new GOTO();
                // case 0xa8:
                // 	return new JSR();
                // case 0xa9:
                // 	return new RET();
                case 0xaa:
                    return new TABLE_SWITCH();
                case 0xab:
                    return new LOOKUP_SWITCH();
                // case 0xac:
                // 	return ireturn;
                // case 0xad:
                // 	return lreturn;
                // case 0xae:
                // 	return freturn;
                // case 0xaf:
                // 	return dreturn;
                // case 0xb0:
                // 	return areturn;
                // case 0xb1:
                // 	return _return;
                case 0xb2:
                    return new GET_STATIC();
                 case 0xb3:
                    return new PUT_STATIC();
                 case 0xb4:
                    return new GET_FIELD();
                 case 0xb5:
                    return new PUT_FIELD();
                case 0xb6:
                    return new INVOKE_VIRTUAL();
                case 0xb7:
                    return new INVOKE_SPECIAL();
                // case 0xb8:
                // 	return new INVOKE_STATIC();
                // case 0xb9:
                // 	return new INVOKE_INTERFACE();
                // case 0xba:
                // 	return new INVOKE_DYNAMIC();
                case 0xbb:
                    return new NEW();
                // case 0xbc:
                // 	return new NEW_ARRAY();
                // case 0xbd:
                // 	return new ANEW_ARRAY();
                // case 0xbe:
                // 	return arraylength;
                // case 0xbf:
                // 	return athrow;
                 case 0xc0:
                    return new CHECK_CAST();
                case 0xc1:
                    return new INSTANCE_OF();
                // case 0xc2:
                // 	return monitorenter;
                // case 0xc3:
                // 	return monitorexit;
                case 0xc4:
                    return new WIDE();
                // case 0xc5:
                // 	return new MULTI_ANEW_ARRAY()
                case 0xc6:
                    return new IFNULL();
                case 0xc7:
                    return new IFNONNULL();
                case 0xc8:
                    return new GOTO_W();
                // case 0xc9:
                // 	return new JSR_W()
                // case 0xca: breakpoint
                // case 0xfe: impdep1
                // case 0xff: impdep2
                default:
                    throw new Exception(string.Format("Unsupported opcode: {0}!", opcode));
            }
        }
    }
}
