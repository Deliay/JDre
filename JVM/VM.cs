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

            #region old test code
            /*
            ReadResult result = cp.ReadClass(cmd.classname);

            if(result.err.Length != 0) Console.WriteLine(result.err);
            else Console.WriteLine(result.stream.Length);

            ClassFile cf = new ClassFile(result.stream);
            cf.Read();
#if (DEBUG)
            Console.WriteLine(cf.ClassName);
            Console.WriteLine("============================");
            Console.WriteLine("Methods count: " + cf.Methods.Count);
            Console.WriteLine("============================");
            Console.WriteLine("Super class:" + cf.SuperClassName);
            Console.WriteLine("============================");
            Console.WriteLine("Interfaces Implements:");
            foreach (var item in cf.InterfaceNames())
            {
                Console.Write(item);
                Console.Write(",");
            }
            Console.WriteLine("\n============================");
            Console.WriteLine("Methods:");
            foreach (var item in cf.Methods)
            {
                Console.WriteLine(item.Name());
            }

#endif
            */
            #endregion
        }
    }

}
