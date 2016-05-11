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
            FieldSpecification fieldSpec = GetFieldSpecification(name, version);

            if (fieldSpec == null)
            {
                fieldSpec = csvReader.ReadFieldSpecification(path);
                fieldSpec.Path = path;
                fieldSpec.Name = name;
                fieldSpec.Version = version;
                fieldSpec.UploadDate = DateTime.Now;
                return specSetupRepo.CreateFieldSpecification(fieldSpec);
            }
            else
            {
                throw new SpecificationSetupException("Name combined with version must be unique");
            }
        }

        public FileSpecification AddFileSpecification(string name, string path, bool isInput, string inDirectoryPath, string archiveDirectoryPath, string errorDirectoryPath, string outDirectoryPath, string version, string fieldSpecification)
        {
            FileSpecification fileSpec = GetFileSpecification(name, version);

            if (fileSpec == null)
            {
                IList<Directory> directories = new List<Directory>();

                if (isInput)
                {
                    if (!System.IO.Directory.Exists(inDirectoryPath))
                    {
                        throw new SpecificationSetupException("Path " + inDirectoryPath + " for the input directory is not a valid path.");
                    }
                    else if (!System.IO.Directory.Exists(archiveDirectoryPath))
                    {
                        throw new SpecificationSetupException("Path " + archiveDirectoryPath + " for the archive directory is not a valid path.");
                    }
                    else if (!System.IO.Directory.Exists(errorDirectoryPath))
                    {
                        throw new SpecificationSetupException("Path " + errorDirectoryPath + " for the error directory is not a valid path.");
                    }
                    else
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
                }
                else
                {
                    if (!System.IO.Directory.Exists(outDirectoryPath))
                    {
                        throw new SpecificationSetupException("Path " + outDirectoryPath + " for the out directory is not a valid path.");
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
                }

                IEnumerable<String> fieldSpecificationProperties = fieldSpecification.Split('-');

                FieldSpecification fieldSpec = GetFieldSpecification(fieldSpecificationProperties.First().Trim(), fieldSpecificationProperties.ElementAt(1).Trim());

                fileSpec = csvReader.ReadFileSpecification(path, fieldSpec);
                fileSpec.Name = name;
                fileSpec.UploadDate = DateTime.Now;
                fileSpec.IsInput = isInput;
                fileSpec.Version = version;
                fileSpec.FieldSpecification = fieldSpec;

                fileSpec.Directories = directories;

                if (fieldSpec.FileSpecifications == null)
                {
                    fieldSpec.FileSpecifications = new List<FileSpecification>();
                }
                fieldSpec.FileSpecifications.Add(fileSpec);
                return specSetupRepo.CreateFileSpecification(fileSpec);
            }
            else
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
            FileSpecification fileSpecification = specSetupRepo.ReadFileSpecification(specificationId);

            if (fileSpecification != null)
            {
                return fileSpecification;
            }
            else
            {
                throw new SpecificationSetupException("The requested File Specification with id " + specificationId + " does not exist!");
            }
        }

        public FileSpecification GetFileSpecification(string name)
        {
            return specSetupRepo.ReadFileSpecification(name);
        }

        public FileSpecification GetFileSpecification(string name, string version)
        {
            return specSetupRepo.ReadFileSpecification(name, version);
        }
        public FileSpecification GetFileSpecificationWithFieldConditions(string name, string version)
        {
            return specSetupRepo.ReadFileSpecificationWithFieldConditions(name, version);
        }

        public FileSpecification GetFileSpecificationAtStartWorkflowTemplateWithName(string specificationName)
        {
            return specSetupRepo.ReadFileSpecificationAtStartWorkflowTemplateWithName(specificationName);
        }

        public FileSpecification GetFileSpecificationWithMessagesAndWorkflowTemplateSteps(int specificationId)
        {
            return specSetupRepo.ReadFileSpecificationWithMessagesAndWorkflowTemplateSteps(specificationId);
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
            int amountOfFileSpecsLinkedToFieldSpec = fieldSpecification.FileSpecifications.Count();
            if (amountOfFileSpecsLinkedToFieldSpec == 0)
            {
                return specSetupRepo.DeleteFieldSpecification(specificationId);
            }
            else
            {
                throw new SpecificationSetupException(
                    String.Format("This field specification cannot be deleted because there are {0} file specifications linked to it!",
                    amountOfFileSpecsLinkedToFieldSpec)
                );
            }
        }

        public FileSpecification RemoveFileSpecification(int specificationId)
        {
            FileSpecification fileSpecification = GetFileSpecificationWithMessagesAndWorkflowTemplateSteps(specificationId);

            if (fileSpecification.Messages.Count() > 0)
            {
                throw new SpecificationSetupException(String.Format("This file specification cannot be deleted because there are {0} messages linked to it. ", fileSpecification.Messages.Count()));
            }
            else if (fileSpecification.WorkflowTemplateSteps.Count() > 0)
            {
                throw new SpecificationSetupException(String.Format("This file specification cannot be deleted because there are {0} workflow template steps linked to it.", fileSpecification.WorkflowTemplateSteps.Count()));
            }
            else
            {
                return specSetupRepo.DeleteFileSpecification(specificationId);
            }
        }

        public List<Directory> GetInputDirectories()
        {
            return specSetupRepo.ReadInputDirectories().ToList();
        }

        public FileSpecification UpdateFileSpecification(FileSpecification fileSpecification)
        {
            return specSetupRepo.UpdateFileSpecification(fileSpecification);
        }

        public AnswerContent AddAnswerContent(string name, string path, string fileSpecificationString)
        {
            AnswerContent answerContent = GetAnswerContent(name);
            if(answerContent == null)
            {
                IEnumerable<String> fileSpecificationProperties = fileSpecificationString.Split('-');
                FileSpecification fileSpec = GetFileSpecificationWithFieldConditions(fileSpecificationProperties.First().Trim(), fileSpecificationProperties.ElementAt(1).Trim());
                answerContent = csvReader.ReadAnswerContent(path, fileSpec);
                answerContent.Name = name;
                answerContent.UploadDate = DateTime.Now;
                answerContent.fileSpecification = fileSpec;
                specSetupRepo.CreateAnswerContent(answerContent);
                return answerContent;
            }
            else
            {
                throw new SpecificationSetupException("Answer Content with name " + answerContent.Name + " already exists.");
            }
            
        }

        public AnswerContent GetAnswerContent(string name)
        {
            return specSetupRepo.ReadAnswerContent(name);
        }
    }
}
