﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIS.BL.Domain.Setup;
using FIS.DAL.EF;

namespace FIS.DAL
{
    public class SpecificationSetupRepository : ISpecificationSetupRepository
    {
        private readonly FISEFDbContext ctx;

        public SpecificationSetupRepository()
        {
            ctx = FISEFDbContext.Instance;
        }

        public FieldSpecification CreateFieldSpecification(FieldSpecification fieldSpecification)
        {
            throw new NotImplementedException();
        }

        public FieldSpecification ReadFieldSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public FieldSpecification ReadFieldSpecification(string fieldSpecificationVersion)
        {
            return ctx.FieldSpecifications.Where(fs => fs.Version.Equals(fieldSpecificationVersion)).First();
        }

        public IEnumerable<FieldSpecification> ReadFieldSpecifications()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<String> ReadFieldSpecificationVersions()
        {
            IList<String> versions = new List<String>();
            var fieldSpecifications = (
                from fieldSpecification in ctx.FieldSpecifications
                select /*fieldSpecification.Version*/ new
                {
                   Version = fieldSpecification.Version 
                }
                ).ToList();
            foreach (var fieldSpecification in fieldSpecifications)
            {
                versions.Add(fieldSpecification.Version);
            }
            return versions;
        }

        public FieldSpecification DeleteFieldSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public FileSpecification CreateFileSpecification(FileSpecification fileSpecification)
        {
            throw new NotImplementedException();
        }

        public FileSpecification ReadFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public FileSpecification ReadFileSpecificationByDirectoryId(int directoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileSpecification> ReadFileSpecifications()
        {
            throw new NotImplementedException();
        }

        public FileSpecification DeleteFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public GroupCondition ReadGroupCondition(int specificationId, string groupCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileSpecFieldCondition> ReadFileSpecFieldConditionsOfGroup(int specificationId, string groupCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileSpecFieldCondition> ReadFileSpecFieldConditionsLinkedToFieldSpecFieldCondition(int fieldConditionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Directory> ReadInputDirectories()
        {
            throw new NotImplementedException();
        }
    }
}
