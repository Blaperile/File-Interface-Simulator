using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util
{
    public interface ISpecificationReader
    {
        FieldSpecification ReadFieldSpecification(string path);
        FileSpecification ReadFileSpecification(string path, FieldSpecification fieldSpecification);
      
    }
}
