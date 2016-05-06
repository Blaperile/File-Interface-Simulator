using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using FIS.BL.Exceptions;
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
        private CSVReader csvReader;
        private ISpecificationSetupRepository specSetupRepo;

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

        public FileSpecification AddFileSpecification(string name, string path, bool isInput, string inDirectoryPath, string archiveDirectoryPath, string errorDirectoryPath, string outDirectoryPath, string version, string fieldSpecification)
        {
            FileSpecification fileSpec = GetFileSpecification(name, version);

            if (fileSpec == null)
            {

                IEnumerable<String> fieldSpecificationProperties = fieldSpecification.Split('-');

                FieldSpecification fieldSpec = GetFieldSpecification(fieldSpecificationProperties.First().Trim(), fieldSpecificationProperties.ElementAt(1).Trim());

                fileSpec = csvReader.ReadFileSpecification(path, fieldSpec);
                fileSpec.Name = name;
                fileSpec.UploadDate = DateTime.Now;
                fileSpec.IsInput = isInput;
                fileSpec.Version = version;
                fileSpec.FieldSpecification = fieldSpec;

                IList<Directory> directories = new List<Directory>();

                if (isInput)
                {

                    Directory inDirectory = new Directory()
                    {
                        Name = "in",
                        Location = inDirectoryPath,
                        FileSpecification = fileSpec
                    };

                    directories.Add(inDirectory);

                    Directory errorDirectory = new Directory()
                    {
                        Name = "error",
                        Location = errorDirectoryPath,
                        FileSpecification = fileSpec
                    };

                    directories.Add(errorDirectory);

                    Directory archiveDirectory = new Directory()
                    {
                        Name = "archive",
                        Location = archiveDirectoryPath,
                        FileSpecification = fileSpec
                    };

                    directories.Add(archiveDirectory);
                }
                else
                {
                    Directory outDirectory = new Directory()
                    {
                        Name = "out",
                        Location = outDirectoryPath,
                        FileSpecification = fileSpec
                    };

                    directories.Add(outDirectory);
                }

                fileSpec.Directories = directories;

                if (fieldSpec.FileSpecifications == null)
                {
                    fieldSpec.FileSpecifications = new List<FileSpecification>();
                }
                fieldSpec.FileSpecifications.Add(fileSpec);
                return specSetupRepo.CreateFileSpecification(fileSpec);
            } else
            {
                throw new SpecificationSetupException("A file specification with name " + fileSpec.Name + " and version " + fileSpec.Version + " already exists.");
            }
        }

        public List<FieldSpecification> GetFieldSpecificatons()
        {
            return specSetupRepo.ReadFieldSpecifications();
        }

        public FieldSpecification GetFieldSpecification(string name, string version)
        {
            return specSetupRepo.ReadFieldSpecification(name, version);
        }

        public FieldSpecification GetFieldSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public FieldSpecification GetFieldSpecificationWithFileSpecifications(int specificationId)
        {
            return specSetupRepo.ReadFieldSpecificationWithFileSpecifications(specificationId);
        }

        public FieldSpecFieldCondition GetFieldSpecFieldCondition(int fieldSpecificationId, string fieldCode)
        {
            return specSetupRepo.ReadFieldSpecFieldCondition(fieldSpecificationId, fieldCode);
        }

        public FileSpecFieldCondition GetFileSpecFieldCondition(int id)
        {
            return specSetupRepo.ReadFileSpecFieldCondition(id);
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
            return specSetupRepo.ReadFileSpecification(specificationId);
        }

        public FileSpecification GetFileSpecification(string name)
        {
            return specSetupRepo.ReadFileSpecification(name);
        }

        public FileSpecification GetFileSpecification(string name, string version)
        {
            return specSetupRepo.ReadFileSpecification(name, version);
        }

        public FileSpecification GetFileSpecificationAtStartWorkflowTemplateWithName(string specificationName)
        {
            return specSetupRepo.ReadFileSpecificationAtStartWorkflowTemplateWithName(specificationName);
        }

        public FileSpecification GetFileSpecificationWithMessages(int specificationId)
        {
            return specSetupRepo.ReadFileSpecificationWithMessages(specificationId);
        }

        public List<FileSpecification> GetFileSpecifications()
        {
            return specSetupRepo.ReadFileSpecifications();
        }

        public GroupCondition GetGroupCondition(int groupConditionId)
        {
            return specSetupRepo.ReadGroupCondition(groupConditionId);
        }

        public FieldSpecification RemoveFieldSpecification(int specificationId)
        {
            FieldSpecification fieldSpecification = GetFieldSpecificationWithFileSpecifications(specificationId);
            if (fieldSpecification.FileSpecifications.Count() == 0)
            {
                return specSetupRepo.DeleteFieldSpecification(specificationId);
            }

            return null;
        }

        public FileSpecification RemoveFileSpecification(int specificationId)
        {
            FileSpecification fileSpecification = GetFileSpecificationWithMessages(specificationId);
            if (fileSpecification.Messages.Count() == 0)
            {
                return specSetupRepo.DeleteFileSpecification(specificationId);
            }

            return null;
        }

        public List<Directory> GetInputDirectories()
        {
            return specSetupRepo.ReadInputDirectories().ToList();
        }
    }
}
