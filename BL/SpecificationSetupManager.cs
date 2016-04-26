using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class SpecificationSetupManager : ISpecificationSetupManager
    {
        public FieldSpecification addFieldSpecification(string name, string path, string version)
        {
            throw new NotImplementedException();
        }

        public FileSpecification addFileSpecification(string name, string path, bool isInput, string inDirectoryPath, string archiveDirectoryPath, string errorDirectoryPath, string outDirectoryPath, string fieldSpecificationVersion)
        {
            throw new NotImplementedException();
        }

        public List<FieldSpecification> getFieldSpecificatins()
        {
            throw new NotImplementedException();
        }

        public FieldSpecification getFieldSpecification(string fieldSpecificationVersion)
        {
            throw new NotImplementedException();
        }

        public FieldSpecification getFieldSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public List<FileSpecFieldFieldCondition> getFileSpecFieldConditionsLinkedToFieldSpecFieldCondition(int fieldSpecFieldConditionId)
        {
            throw new NotImplementedException();
        }

        public List<FileSpecFieldCondition> getFileSpecFieldConditionsOfGroup(int specificationId, string groupCode)
        {
            throw new NotImplementedException();
        }

        public FileSpecification getFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public List<FileSpecification> getFileSpecifications()
        {
            throw new NotImplementedException();
        }

        public GroupCondition getGroupCondition(int specificationId, string groupCode)
        {
            throw new NotImplementedException();
        }

        public FieldSpecification removeFieldSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public FileSpecification removeFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }
    }
}
