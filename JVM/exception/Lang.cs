using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.exception.Lang
{
    [Serializable]
    class ClassFormatError : Exception
    {
        public ClassFormatError(string message) : base(message) { }
    }
    [Serializable]
    class UnsupportedClassVersionError : Exception { }
    [Serializable]
    class NullPointerException : Exception { }
    [Serializable]
    class InstantiationError : Exception { }
    [Serializable]
    class IllegalAccessError : Exception { }
    [Serializable]
    class ClassCastException : Exception { }
    [Serializable]
    class ClsasFormatError : Exception { }
    [Serializable]
    class IncompatiableClassChangeError : Exception { }
    [Serializable]
    class NoSuchMethodError : Exception { }
    [Serializable]
    class AbstractMethodError : Exception { }
}
