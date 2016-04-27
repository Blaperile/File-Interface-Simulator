using FIS.BL.Domain.Setup;
using FIS.BL.Util.CSV;
using FIS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL
{
    public class SpecificationSetupManager : ISpecificationSetupManager
    {
        CSVReader csvReader;
        ISpecificationSetupRepository specSetupRepo;

        public SpecificationSetupManager()
        {
            csvReader = new CSVReader(this);
            specSetupRepo = new SpecificationSetupRepository();
        }

        public FieldSpecification AddFieldSpecification(string name, string path, string version)
        {
            FieldSpecification fieldSpec = csvReader.ReadFieldSpecification(path);
            fieldSpec.Path = path;
            fieldSpec.Name = name;
            fieldSpec.Version = version;
            fieldSpec.UploadDate = DateTime.Now;
            return specSetupRepo.CreateFieldSpecification(fieldSpec);
        }

        public FileSpecification AddFileSpecification(string name, string path, bool isInput, string inDirectoryPath, string archiveDirectoryPath, string errorDirectoryPath, string outDirectoryPath, string version, string fieldSpecificationVersion)
        {
            FieldSpecification fieldSpec = GetFieldSpecification(fieldSpecificationVersion);
            FileSpecification fileSpec = csvReader.ReadFileSpecification(path, fieldSpec);
            fileSpec.Name = name;
            fileSpec.UploadDate = DateTime.Now;
            fileSpec.IsInput = isInput;
            fileSpec.Version = version;
            fileSpec.FieldSpecification = fieldSpec;

            if (isInput)
            {

                Directory inDirectory = new Directory()
                {
                    Name = "in",
                    Location = inDirectoryPath,
                    FileSpecification = fileSpec
                };

                fileSpec.InDirectory = inDirectory;

                Directory errorDirectory = new Directory()
                {
                    Name = "error",
                    Location = errorDirectoryPath,
                    FileSpecification = fileSpec
                };

                fileSpec.ErrorDirectory = errorDirectory;

                Directory archiveDirectory = new Directory()
                {
                    Name = "archive",
                    Location = archiveDirectoryPath,
                    FileSpecification = fileSpec
                };

                fileSpec.ArchiveDirectory = archiveDirectory;
            }
            else
            {
                Directory outDirectory = new Directory()
                {
                    Name = "out",
                    Location = outDirectoryPath,
                    FileSpecification = fileSpec
                };

                fileSpec.OutDirectory = outDirectory;
            }

            if (fieldSpec.FileSpecifications == null)
            {
                fieldSpec.FileSpecifications = new List<FileSpecification>();
            }
            fieldSpec.FileSpecifications.Add(fileSpec);
            return specSetupRepo.CreateFileSpecification(fileSpec);
        }

        public List<FieldSpecification> GetFieldSpecificatons()
        {
            throw new NotImplementedException();
        }

        public FieldSpecification GetFieldSpecification(string fieldSpecificationVersion)
        {
            return specSetupRepo.ReadFieldSpecification(fieldSpecificationVersion);
        }

        public FieldSpecification GetFieldSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public FieldSpecFieldCondition GetFieldSpecFieldCondition(int fieldSpecificationId, string fieldCode)
        {
            return specSetupRepo.ReadFieldSpecFieldCondition(fieldSpecificationId, fieldCode);
        }

        public IEnumerable<String> GetFieldSpecificationVersions()
        {
            return specSetupRepo.ReadFieldSpecificationVersions();
        }

        public List<FileSpecFieldCondition> GetFileSpecFieldConditionsLinkedToFieldSpecFieldCondition(int fieldSpecFieldConditionId)
        {
            throw new NotImplementedException();
        }

        public List<FileSpecFieldCondition> GetFileSpecFieldConditionsOfGroup(int specificationId, string groupCode)
        {
            throw new NotImplementedException();
        }

        public FileSpecification GetFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public List<FileSpecification> GetFileSpecifications()
        {
            throw new NotImplementedException();
        }

        public GroupCondition GetGroupCondition(int specificationId, string groupCode)
        {
            throw new NotImplementedException();
        }

        public FieldSpecification RemoveFieldSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public FileSpecification RemoveFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }
    }
}
