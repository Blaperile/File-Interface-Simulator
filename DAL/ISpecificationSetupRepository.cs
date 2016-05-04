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
        FileSpecification ReadFileSpecificationAtStartWorkflowTemplateWithName(string specificationName);
        List<FileSpecification> ReadFileSpecifications();
        FileSpecification DeleteFileSpecification(int specificationId);

        GroupCondition ReadGroupCondition(int groupConditionId);
        FieldSpecFieldCondition ReadFieldSpecFieldCondition(int fieldSpecificationId, string fieldCode);
        FileSpecFieldCondition ReadFileSpecFieldCondition(int id);
        IEnumerable<FileSpecFieldCondition> ReadFileSpecFieldConditionsOfGroup(int specificationId, string groupCode);
        IEnumerable<FileSpecFieldCondition> ReadFileSpecFieldConditionsLinkedToFieldSpecFieldCondition(int fieldConditionId);

        IEnumerable<Directory> ReadInputDirectories();
    }
}
