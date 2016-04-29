using System;
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
            if (ctx.FieldSpecifications.Any(f => f.Name == fieldSpecification.Name) && ctx.FieldSpecifications.Any(f => f.Version == fieldSpecification.Version))
            {
                return null;
            }
            ctx.FieldSpecifications.Add(fieldSpecification);
            ctx.SaveChanges();
            return fieldSpecification;
        }

        public FieldSpecification ReadFieldSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public FieldSpecification ReadFieldSpecification(string name, string version)
        {
            return ctx.FieldSpecifications.Where(fs => fs.Name.Equals(name)).Where(fs => fs.Version.Equals(version)).First();
        }

        public List<FieldSpecification> ReadFieldSpecifications()
        {
            return ctx.FieldSpecifications.ToList();
        }

        public FieldSpecification DeleteFieldSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public FileSpecification CreateFileSpecification(FileSpecification fileSpecification)
        {
            ctx.FileSpecifications.Add(fileSpecification);
            ctx.SaveChanges();
            return fileSpecification;
        }

        public FileSpecification ReadFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public FileSpecification ReadFileSpecification(string name)
        {
            IEnumerable<FileSpecification> fileSpecifications = ctx.FileSpecifications.Where(fs => fs.Name.Equals(name));
            fileSpecifications = fileSpecifications.OrderByDescending(fs => fs.UploadDate).ToList();
            return fileSpecifications.First();
        }

        public FileSpecification ReadFileSpecification(string name, string version)
        {
            IEnumerable<FileSpecification> fileSpecifications = ctx.FileSpecifications.Where(fs => fs.Name.Equals(name)).Where(fs => fs.Version.Equals(version));
            if (fileSpecifications.Count() == 0) return null; 
            return fileSpecifications.First();
        }

        public FileSpecification ReadFileSpecificationByDirectoryId(int directoryId)
        {
            throw new NotImplementedException();
        }

        public List<FileSpecification> ReadFileSpecifications()
        {
            return ctx.FileSpecifications.ToList();
        }

        public FileSpecification DeleteFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public GroupCondition ReadGroupCondition(int specificationId, string groupCode)
        {
            throw new NotImplementedException();
        }

        public FieldSpecFieldCondition ReadFieldSpecFieldCondition(int fieldSpecificationId, string fieldCode)
        {
            IEnumerable<FieldSpecFieldCondition> fieldSpecFieldConditions = ctx.FieldSpecFieldConditions.Where(fsfc => fsfc.FieldSpecification.FieldSpecificationId == fieldSpecificationId).Where(fsfc => fsfc.FieldCode == fieldCode);
            if (fieldSpecFieldConditions.Count() == 0) return null;
            return fieldSpecFieldConditions.First();
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
