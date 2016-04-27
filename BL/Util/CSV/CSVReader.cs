using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIS.BL.Domain.Setup;

namespace FIS.BL.Util.CSV
{
    public class CSVReader : ISpecificationReader
    {
        public FieldSpecification ReadFieldSpecification(string path)
        {
            throw new NotImplementedException();
        }

        public FileSpecification ReadFileSpecification(string path, FieldSpecification fieldSpecification)
        {
            throw new NotImplementedException();
        }
    }
}
