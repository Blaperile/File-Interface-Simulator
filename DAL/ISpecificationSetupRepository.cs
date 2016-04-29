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
        FieldSpecification ReadFieldSpecification(string name, string version);
        List<FieldSpecification> ReadFieldSpecifications();
        FieldSpecification DeleteFieldSpecification(int specificationId);

        FileSpecification CreateFileSpecification(FileSpecification fileSpecification);
        FileSpecification ReadFileSpecification(int specificationId);
        FileSpecification ReadFileSpecification(string name, string version);
        FileSpecification ReadFileSpecification(string name);
        FileSpecification ReadFileSpecificationByDirectoryId(int directoryId);
        List<FileSpecification> ReadFileSpecifications();
        FileSpecification DeleteFileSpecification(int specificationId);

        GroupCondition ReadGroupCondition(int specificationId, string groupCode);
        FieldSpecFieldCondition ReadFieldSpecFieldCondition(int fieldSpecificationId, string fieldCode);
        IEnumerable<FileSpecFieldCondition> ReadFileSpecFieldConditionsOfGroup(int specificationId, string groupCode);
        IEnumerable<FileSpecFieldCondition> ReadFileSpecFieldConditionsLinkedToFieldSpecFieldCondition(int fieldConditionId);

        IEnumerable<Directory> ReadInputDirectories();
    }
}
