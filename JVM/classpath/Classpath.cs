using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.classpath
{
    class Classpath
    {
        Entry boot, ext, user;

        public Classpath(string jreOption, string cpOption)
        {
            ParseBootAndExtClassPath(jreOption);
            ParserUserClasspath(cpOption);
        }

        public ReadResult ReadClass(string className)
        {
            string fn = className.Replace('.', '/') + ".class";
            ReadResult result;
            result = boot.readClass(fn);
            if (result.err == string.Empty) return result;

            result = ext.readClass(fn);
            if (result.err == string.Empty) return result;

            return user.readClass(fn);

        }

        public override string ToString()
        {
            return user.ToString();
        }

        private void ParserUserClasspath(string cpOption)
        {
            if (cpOption.Length == 0) user = Entry.CreateEntryByString(".");
            else user = Entry.CreateEntryByString(cpOption);
        }
    
        private void ParseBootAndExtClassPath(string jreOption)
        {
            var jd = getJreDir(jreOption);
            // jre/lib/*
            boot = Entry.CreateEntryByString(Path.Combine(jd, "lib", "*"));
            // jre/ext/lib/*
            ext = Entry.CreateEntryByString(Path.Combine(jd, "lib", "ext", "*"));
        }

        private string getJreDir(string jreOption)
        {
            if (jreOption.Length != 0 && Directory.Exists(jreOption)) return jreOption;
            else if (Directory.Exists(@".\jre")) return @".\jre";
            string jh = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (jh.Length > 0) return Path.Combine(jh, @"jre");

            throw new Exception("jre cannot found!");
        }
    }
}
