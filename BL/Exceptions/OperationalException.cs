using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Exceptions
{
    public class OperationalException : Exception
    {
        public OperationalException(string message) : base(message) { }

    }
}
