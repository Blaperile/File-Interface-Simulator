using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIS.BL.Domain.Setup;
using FIS.DAL.EF;
using FIS.BL.Domain.Operational;
using System.Data.Entity;

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

        public FieldSpecification ReadFieldSpecificationWithFileSpecifications(int specificationId)
        {
            FieldSpecification fieldSpecification = ctx.FieldSpecifications.Find(specificationId);
            ctx.Entry<FieldSpecification>(fieldSpecification).Collection<FileSpecification>(fs => fs.FileSpecifications).Load();
            return fieldSpecification;
        }

        public List<FieldSpecification> ReadFieldSpecifications()
        {
            return ctx.FieldSpecifications.ToList();
        }

        public FieldSpecification DeleteFieldSpecification(int specificationId)
        {
            FieldSpecification fieldSpecification = ctx.FieldSpecifications.Find(specificationId);
            ctx.FieldSpecifications.Remove(fieldSpecification);
            ctx.SaveChanges();
            return fieldSpecification;
        }

        public FileSpecification CreateFileSpecification(FileSpecification fileSpecification)
        {
            ctx.FileSpecifications.Add(fileSpecification);
            ctx.SaveChanges();
            return fileSpecification;
        }

        public FileSpecification ReadFileSpecification(int specificationId)
        {
             FileSpecification fileSpec = ctx.FileSpecifications.FirstOrDefault(f => f.FileSpecificationId == specificationId);
            ctx.Entry<FileSpecification>(fileSpec).Collection<Directory>(f => f.Directories).Load();
            LoadHeaderConditions(fileSpec);
            LoadGroupConditions(fileSpec);
             return fileSpec;
        }

        public void LoadHeaderConditions(FileSpecification fileSpecification)
        {
            ctx.Entry<FileSpecification>(fileSpecification).Collection<HeaderCondition>(f => f.HeaderConditions).Load();
        }

        public void LoadGroupConditions(FileSpecification fileSpecification)
        {
            ctx.Entry<FileSpecification>(fileSpecification).Collection<GroupCondition>(f => f.GroupConditions).Load();
            foreach(GroupCondition groupCondition in fileSpecification.GroupConditions)
            {
                LoadFieldConditions(groupCondition);
            }
        }

        private void LoadFieldConditions(GroupCondition groupCondition)
        {
            ctx.Entry<GroupCondition>(groupCondition).Collection<FileSpecFieldCondition>(g => g.FileSpecFieldConditions).Load();
            foreach(FileSpecFieldCondition fileSpecFieldCondition in groupCondition.FileSpecFieldConditions)
            {
                LoadFieldSpecFieldCondition(fileSpecFieldCondition);
            }
        }

        private void LoadFieldSpecFieldCondition(FileSpecFieldCondition fileSpecFieldCondition)
        {
            ctx.Entry<FileSpecFieldCondition>(fileSpecFieldCondition).Reference<FieldSpecFieldCondition>(f => f.FieldSpecFieldCondition).Load();
            ctx.Entry<FieldSpecFieldCondition>(fileSpecFieldCondition.FieldSpecFieldCondition).Collection<AllowedValue>(f => f.AllowedValues).Load();
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

       public FileSpecification ReadFileSpecificationAtStartWorkflowTemplateWithName(string specificationName)
        {
   
            WorkflowTemplateStep workflowTemplateStep = ctx.WorkflowTemplateSteps.Where(wt => wt.StepNumber == 1).Where(wt => wt.fileSpecification.Name.Equals(specificationName)).Where(wt => wt.WorkflowTemplate.IsChosen == true).FirstOrDefault();
            if (workflowTemplateStep != null)
            {
                ctx.Entry<WorkflowTemplateStep>(workflowTemplateStep).Reference<FileSpecification>(wt => wt.fileSpecification).Load();
                FileSpecification fileSpecification = workflowTemplateStep.fileSpecification;

                ctx.Entry<FileSpecification>(fileSpecification).Collection<HeaderCondition>(fs => fs.HeaderConditions).Load();
                ctx.Entry<FileSpecification>(fileSpecification).Collection<GroupCondition>(fs => fs.GroupConditions).Load();
                LoadFileSpecFieldConditions(fileSpecification);
                ctx.Entry<FileSpecification>(fileSpecification).Collection<Directory>(fs => fs.Directories).Load();
                ctx.Entry<FileSpecification>(fileSpecification).Collection<Message>(fs => fs.Messages).Load();
                ctx.Entry<FileSpecification>(fileSpecification).Collection<WorkflowTemplateStep>(fs => fs.WorkflowTemplateSteps).Load();
                return fileSpecification;
            }
            return null;
        }

        public FileSpecification ReadFileSpecificationWithMessages(int specificationId)
        {
            FileSpecification fileSpecification = ctx.FileSpecifications.Find(specificationId);
            ctx.Entry<FileSpecification>(fileSpecification).Collection<Message>(fs => fs.Messages).Load();
            return fileSpecification;
        }

        private void LoadFileSpecFieldConditions(FileSpecification fileSpecification)
        {
            ctx.Entry<FileSpecification>(fileSpecification).Collection<FileSpecFieldCondition>(fs => fs.FileSpecFieldConditions).Load();
            foreach (FileSpecFieldCondition fileSpecFieldCondition in fileSpecification.FileSpecFieldConditions)
            {
                ctx.Entry<FileSpecFieldCondition>(fileSpecFieldCondition).Reference<FieldSpecFieldCondition>(fc => fc.FieldSpecFieldCondition).Load();
            }
        }

        public List<FileSpecification> ReadFileSpecifications()
        {
            return ctx.FileSpecifications.ToList();
        }

        public FileSpecification DeleteFileSpecification(int specificationId)
        {
            FileSpecification fileSpecification = ctx.FileSpecifications.Find(specificationId);
            ctx.FileSpecifications.Remove(fileSpecification);
            ctx.SaveChanges();
            return fileSpecification;
        }

        public GroupCondition ReadGroupCondition(int groupConditionId)
        {
            GroupCondition groupCondition = ctx.GroupConditions.Find(groupConditionId);
            ctx.Entry<GroupCondition>(groupCondition).Collection<FileSpecFieldCondition>(gc => gc.FileSpecFieldConditions).Load();
            LoadFieldConditions(groupCondition);
            return groupCondition;
        }

        public FieldSpecFieldCondition ReadFieldSpecFieldCondition(int fieldSpecificationId, string fieldCode)
        {
            IEnumerable<FieldSpecFieldCondition> fieldSpecFieldConditions = ctx.FieldSpecFieldConditions.Where(fsfc => fsfc.FieldSpecification.FieldSpecificationId == fieldSpecificationId).Where(fsfc => fsfc.FieldCode == fieldCode);
            if (fieldSpecFieldConditions.Count() == 0) return null;
            return fieldSpecFieldConditions.First();
        }

        public FileSpecFieldCondition ReadFileSpecFieldCondition(int id)
        {
            FileSpecFieldCondition fileSpecFieldCondition = ctx.FileSpecFieldConditions.Find(id);
            LoadFieldSpecFieldCondition(fileSpecFieldCondition);
            return fileSpecFieldCondition;
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
            return ctx.Directories.Where(d => d.Name.Equals("in"));
        }

        public FileSpecification UpdateFileSpecification(FileSpecification fileSpecification)
        {
            ctx.FileSpecifications.Attach(fileSpecification);
            ctx.Entry(fileSpecification).State = EntityState.Modified;
            ctx.SaveChanges();
            return fileSpecification;
        }
    }
}
