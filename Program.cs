using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE
{
    class Program
    {
        static JVM.VM vm;

        static void Main(string[] args)
        {
            CommandParser cmd = new CommandParser(args);
            
            if (cmd.isVersion) cmd.version();
            else if (cmd.isHelp) cmd.usage();
            else if (cmd.isStart) StartJVM(cmd);
        }

        static void StartJVM(CommandParser cmds)
        {
            vm = new JVM.VM(cmds);
            vm.StartJVM();
        }
    }
}
