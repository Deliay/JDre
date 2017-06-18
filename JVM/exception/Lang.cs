using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDRE.JVM.exception.Lang
{
    class ClassFormatError : Exception
    {
        public ClassFormatError(string message) : base(message) { }
    }

    class UnsupportedClassVersionError : Exception { }

    class NullPointerException : Exception { }

    class InstantiationError : Exception { }

    class IllegalAccessError : Exception { }

    class ClassCastException : Exception { }

    class ClsasFormatError : Exception { }

    class IncompatiableClassChangeError : Exception { }
}
