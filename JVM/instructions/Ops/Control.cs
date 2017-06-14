using JDRE.JVM.instructions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDRE.JVM.runtime;

namespace JDRE.JVM.instructions.Control
{ 
    //无条件goto跳转指令
    class GOTO : BranchInstruction
    {
        public override void Execute(Frame frame)
        {
            BranchJump(frame, Offset);
        }
    }

    //switch - case
    //tableswitch指令
    class TABLE_SWITCH : Instruction
    {
        int defaultOffset;
        int low;
        int high;
        int[] jumpOffsets;

        public void Execute(Frame frame)
        {
            int index = frame.OperandStack.PopInt32();
            int offset;
            if(index >= low && index <= high)
            {
                offset = jumpOffsets[index - low];
            }
            else
            {
                offset = defaultOffset;
            }
            BranchInstruction.BranchJump(frame, offset);
        }

        public void FetchOperands(BytecodeReader reader)
        {
            reader.SkipPadding();
            defaultOffset = reader.ReadInt32();
            //对于switch来说low和high记录case的取值范围
            //offsets里存放high-low+1个int值，存放各种case情况下，跳转所需的偏移量
            low = reader.ReadInt32();
            high = reader.ReadInt32();
            jumpOffsets = reader.ReadInt32s(high - low + 1);           
        }
    }
    //lookupswitch指令
    class LOOKUP_SWITCH : Instruction
    {
        int defaultOffset;
        int npairs;
        int[] matchOffsets;

        public void Execute(Frame frame)
        {
            //先从栈顶弹出一个int，然后查找matchOffsets，如果可以，直接跳转到其值
            //否则就走defualt
            int key = frame.OperandStack.PopInt32();
            int offset = defaultOffset;
            for (int i = 0; i < npairs * 2; i++)
            {
                if(matchOffsets[i] == key)
                {
                    offset = matchOffsets[i];
                    break;
                }
            }
            BranchInstruction.BranchJump(frame, offset);
        }

        public void FetchOperands(BytecodeReader reader)
        {
            reader.SkipPadding();
            defaultOffset = reader.ReadInt32();
            npairs = reader.ReadInt32();
            matchOffsets = reader.ReadInt32s(npairs * 2);
            //lookupswitch指令比较像Map，key值是case，value是偏移

        }
    }
}
