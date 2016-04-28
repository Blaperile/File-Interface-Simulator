using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Exceptions
{
    public class FileReadException : Exception
    {
        public  FileReadException(string message) : base(message) { }
    }
}
