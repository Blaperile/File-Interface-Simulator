using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL
{
    public interface ISpecificationSetupManager
    {
        FieldSpecification AddFieldSpecification(String name, String path, String version);
        FieldSpecification GetFieldSpecification(int specificationId);
        FieldSpecification GetFieldSpecification(String fieldSpecificationVersion);
        List<FieldSpecification> GetFieldSpecificatons();
        IEnumerable<String> GetFieldSpecificationVersions();
        FieldSpecification RemoveFieldSpecification(int specificationId);
        FileSpecification AddFileSpecification(String name, String path, bool isInput, String inDirectoryPath, String archiveDirectoryPath, String errorDirectoryPath, String outDirectoryPath, String fieldSpecificationVersion);
        FileSpecification GetFileSpecification(int specificationId);
        List<FileSpecification> GetFileSpecifications();
        FileSpecification RemoveFileSpecification(int specificationId);
        GroupCondition GetGroupCondition(int specificationId, String groupCode);
        FieldSpecFieldCondition GetFieldSpecFieldCondition(int fieldSpecificationId, string fieldCode);
        List<FileSpecFieldCondition> GetFileSpecFieldConditionsOfGroup(int specificationId, String groupCode);
        List<FileSpecFieldCondition> GetFileSpecFieldConditionsLinkedToFieldSpecFieldCondition(int fieldSpecFieldConditionId);
    }
}
