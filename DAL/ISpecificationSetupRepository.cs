using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.DAL
{
    public interface ISpecificationSetupRepository
    {
        FieldSpecification CreateFieldSpecification(FieldSpecification fieldSpecification);
        FieldSpecification ReadFieldSpecification(int specificationId);
        FieldSpecification ReadFieldSpecification(string fieldSpecificationVersion);
        IEnumerable<FieldSpecification> ReadFieldSpecifications();
        FileSpecification CreateFileSpecification(FileSpecification fileSpecification);
        FileSpecification ReadFileSpecification(int specificationId);
        FileSpecification ReadFileSpecificationByDirectoryId(int directoryId);
    }
}
