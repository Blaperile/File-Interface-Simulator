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
        FieldSpecification ReadFieldSpecificationWithFileSpecifications(int specificationId);
        List<FieldSpecification> ReadFieldSpecifications();
        FieldSpecification DeleteFieldSpecification(int specificationId);

        FileSpecification CreateFileSpecification(FileSpecification fileSpecification);
        FileSpecification ReadFileSpecification(int specificationId);
        FileSpecification ReadFileSpecification(string name, string version);
        FileSpecification ReadFileSpecification(string name);
        FileSpecification ReadFileSpecificationWithFieldConditions(string name, string version);
        FileSpecification ReadFileSpecificationByDirectoryId(int directoryId);
        FileSpecification ReadFileSpecificationAtStartWorkflowTemplateWithName(string specificationName);
        FileSpecification ReadFileSpecificationWithMessages(int specificationId);
        List<FileSpecification> ReadFileSpecifications();
        FileSpecification DeleteFileSpecification(int specificationId);
        FileSpecification UpdateFileSpecification(FileSpecification fileSpecification);

        void LoadHeaderConditions(FileSpecification fileSpecification);
        void LoadGroupConditions(FileSpecification fileSpecification);

        GroupCondition ReadGroupCondition(int groupConditionId);
        FieldSpecFieldCondition ReadFieldSpecFieldCondition(int fieldSpecificationId, string fieldCode);
        FileSpecFieldCondition ReadFileSpecFieldCondition(int id);
        IEnumerable<FileSpecFieldCondition> ReadFileSpecFieldConditionsOfGroup(int specificationId, string groupCode);
        IEnumerable<FileSpecFieldCondition> ReadFileSpecFieldConditionsLinkedToFieldSpecFieldCondition(int fieldConditionId);

        IEnumerable<Directory> ReadInputDirectories();

        AnswerContent ReadAnswerContent(string name);
        AnswerContent CreateAnswerContent(AnswerContent answerContent);
    }
}
