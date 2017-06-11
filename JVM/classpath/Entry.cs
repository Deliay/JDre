using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.classpath
{
    struct ReadResult
    {
        public Entry ent;
        public string err;
        public Stream stream;
    }

    abstract class Entry
    {
        public static Entry CreateEntryByString(string path)
        {
            string lower = path.ToLower();
            if (Environment.GetEnvironmentVariables().Contains(path)) return new CompositeEntry(path.Split(';'));
            else if (path.EndsWith("*")) return new WildcardEntry(path);
            else if (lower.EndsWith(".jar") || lower.EndsWith(".zip")) return new ZipEntry(path);
            else return new DirEntry(path);

        }
        public abstract ReadResult readClass(string className);
        public override abstract string ToString();
    }

    class DirEntry : Entry
    {
        private string absPath = string.Empty;
        public DirEntry(string path)
        {
            try
            {
                absPath = Path.GetFullPath(path);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{0} ({1})", e.Message, absPath), e);
            }

        }
        public override ReadResult readClass(string className)
        {
            string realPath = Path.Combine(absPath, className);//.Replace('/', '\\'));
            try
            {
                Stream stream = File.OpenRead(realPath);
                return new ReadResult()
                { /*bytes = File.ReadAllBytes(realPath),*/ ent = this, err = string.Empty, stream = stream };
            }
            catch
            {
                //throw new Exception(string.Format("{0} ({1})", e.Message, absPath), e);
                return new ReadResult() { err = "class not found: " + className };
            }
        }

        public override string ToString()
        {
            return absPath;
        }
    }

    class ZipEntry : Entry
    {
        private string absPath;
        public ZipEntry(string path)
        {
            try
            {
                absPath = Path.GetFullPath(path);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("{0} ({1})", e.Message, absPath), e);
            }
        }
        public override ReadResult readClass(string className)
        {
            try
            {
                using (FileStream zipStream = new FileStream(absPath, FileMode.Open))
                {
                    using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Read))
                    {
                        var entry = zip.GetEntry(className);
                        Stream stream = entry.Open();
                        byte[] buf = new byte[entry.Length];
                        stream.Read(buf, 0, (int)entry.Length);

                        File.WriteAllBytes(@"D:\1.class", buf);

                        return new ReadResult() { /*bytes = buf,*/ ent = this, err = string.Empty, stream = new MemoryStream(buf) };
                    }
                }
            }
            catch
            {
                return new ReadResult() { err = "class not found: " + className };
            }
        }

        public override string ToString()
        {
            return absPath;
        }
    }

    class CompositeEntry : Entry
    {
        protected List<Entry> subEntries;
        public CompositeEntry(string[] lists)
        {
            subEntries = new List<Entry>();
            foreach (var item in lists)
            {
                subEntries.Add(CreateEntryByString(item));
            }
        }
        public override ReadResult readClass(string className)
        {
            foreach (var item in subEntries)
            {
                ReadResult result = item.readClass(className);
                if (result.err == string.Empty) return result;
            }

            return new ReadResult() { err = "class not found: " + className };
        }

        public override string ToString()
        {
            string val = string.Empty;
            foreach (var item in subEntries)
            {
                val += item.ToString();
            }
            return val;
        }
    }

    class WildcardEntry : CompositeEntry
    {
        public WildcardEntry(string path) : base(Directory.GetFiles(path.Remove(path.Length - 1), "*.jar", SearchOption.TopDirectoryOnly))
        {

        }
    }

}
