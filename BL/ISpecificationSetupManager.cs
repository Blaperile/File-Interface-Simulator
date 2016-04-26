using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    interface ISpecificationSetupManager
    {
        FieldSpecification addFieldSpecification(String name, String path, String version);
        FieldSpecification getFieldSpecification(int specificationId);
        FieldSpecification getFieldSpecification(String fieldSpecificationVersion);
        List<FieldSpecification> getFieldSpecificatins();
        FieldSpecification removeFieldSpecification(int specificationId);
        FileSpecification addFileSpecification(String name, String path, bool isInput, String inDirectoryPath, String archiveDirectoryPath, String errorDirectoryPath, String outDirectoryPath, String fieldSpecificationVersion);
        FileSpecification getFileSpecification(int specificationId);
        List<FileSpecification> getFileSpecifications();
        FileSpecification removeFileSpecification(int specificationId);
        GroupCondition getGroupCondition(int specificationId, String groupCode);
        List<FileSpecFieldCondition> getFileSpecFieldConditionsOfGroup(int specificationId, String groupCode);
        List<FileSpecFieldFieldCondition> getFileSpecFieldConditionsLinkedToFieldSpecFieldCondition(int fieldSpecFieldConditionId);

    }
}
