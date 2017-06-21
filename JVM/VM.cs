using JDRE.JVM.classfile;
using JDRE.JVM.classpath;
using JDRE.JVM.runtime.Heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM
{
    class VM
    {
        Classpath cp;
        CommandParser cmd;

        public VM(CommandParser cmds)
        {
            cmd = cmds;
        }

        public void StartJVM()
        {
            cp = new Classpath(cmd.XjreOption, cmd.classpath);
            ClassLoader loader = new ClassLoader(cp);

            Console.WriteLine("classpath:{0} class:{1} args:{2}", cp, cmd.classname, cmd.args);

            string className = cmd.classname.Replace('.', '/');
            Class mainClass = loader.LoadClass(className);
            Method mainMethod = mainClass.getMainMethod();

            if(mainMethod != null)
            {
                new interpreter.Interpreter(mainMethod);
            }
            else
            {
                Console.WriteLine("Main method not exist in class " + className);
            }
        }
    }

}
