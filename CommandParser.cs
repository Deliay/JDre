using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE
{
    class CommandParser
    {
        CommandReader reader;
        public bool isVersion, isHelp, isStart;
        public string classpath = string.Empty;
        public string XjreOption = string.Empty;
        public string classname = string.Empty;
        public string args = string.Empty;
        public CommandParser(string[] initialCmds)
        {
            reader = new CommandReader(initialCmds);
            string classpath = null;
            if (reader.checkArg("-version"))
            {
                isVersion = true;
            }
            else if(reader.checkArg("-help") || reader.checkArg("-?"))
            {
                isHelp = true;
            }
            else 
            {
                isStart = reader.fetchArg("-classpath", out classpath) || reader.fetchArg("-cp", out classpath);
                reader.fetchArg("-Xjre", out XjreOption);
            }

            isHelp = !reader.fetch(out classname);
            isStart = !isHelp;
            args = reader.fetchAll();
        }

        public void version()
        {
            Console.WriteLine("version 0.0.1\n");
        }

        public void usage()
        {
            Console.WriteLine("Usage: jdre.exe [-options] class [args...]\n");
        }

    }

    class CommandReader
    {
        private string[] cmds;
        private int curIndex = 0;
        public CommandReader(string[] initialCmds)
        {
            cmds = initialCmds;
        }

        public bool checkArg(string arg)
        {
            if (curIndex <= cmds.Length && cmds[curIndex] == arg)
            {
                curIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool fetchArg(string arg, out string target)
        {
            if (curIndex < cmds.Length && cmds[curIndex] == arg)
            {
                target = cmds[(++curIndex)];
                curIndex++;
                return true;
            }
            else
            {
                target = string.Empty;
                return false;
            }
        }

        public bool fetch(out string target)
        {
            if(curIndex <= cmds.Length)
            {
                target = cmds[curIndex];
                curIndex++;
                return true;
            }
            target = string.Empty;
            return false;
        }

        public string fetchAll()
        {
            string last = "\"";
            while (curIndex < cmds.Length)
            {
                last += cmds[curIndex];
                curIndex++;
            }
            return last + "\"";
        }
    }
}
