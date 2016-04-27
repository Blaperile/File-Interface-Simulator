﻿using FIS.BL.Domain.Setup;
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
            csvReader = new CSVReader();
            specSetupRepo = new SpecificationSetupRepository();
        }

        public FieldSpecification AddFieldSpecification(string name, string path, string version)
        {
            FieldSpecification fieldSpec = csvReader.ReadFieldSpecification(path);
            fieldSpec.Name = name;
            fieldSpec.Version = version;
            fieldSpec.UploadDate = DateTime.Now;
            return fieldSpec;
        }

        public FileSpecification AddFileSpecification(string name, string path, bool isInput, string inDirectoryPath, string archiveDirectoryPath, string errorDirectoryPath, string outDirectoryPath, string fieldSpecificationVersion)
        {
            FieldSpecification fieldSpec = GetFieldSpecification(fieldSpecificationVersion);
            FileSpecification fileSpec = csvReader.ReadFileSpecification(path, fieldSpec);

            Directory inDirectory = new Directory()
            {
                 Name = "in",
                 Location = inDirectoryPath
            };

            fileSpec.InDirectory = inDirectory;

            Directory errorDirectory = new Directory()
            {
                Name = "error",
                Location = errorDirectoryPath
            };

            fileSpec.ErrorDirectory = errorDirectory;

            Directory archiveDirectory = new Directory()
            {
                Name = "archive",
                Location = archiveDirectoryPath
            };

            fileSpec.ArchiveDirectory = archiveDirectory;

            Directory outDirectory = new Directory()
            {
                Name = "out",
                Location = outDirectoryPath
            };

            fileSpec.OutDirectory = outDirectory;

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

        public IEnumerable<String> GetFieldSpecificationVersions()
        {
            throw new NotImplementedException();
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
