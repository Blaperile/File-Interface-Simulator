using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Exceptions
{
    public class SpecificationSetupException : Exception
    {
        public SpecificationSetupException(string message) : base(message) { }
      
    }
}
