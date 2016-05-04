using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIS.BL.Domain.Setup;
using FIS.DAL.EF;
using System.Data.Entity;

namespace FIS.DAL
{
    public class WorkflowTemplateSetupRepository : IWorkflowTemplateSetupRepository
    {
        private readonly FISEFDbContext ctx;

        public WorkflowTemplateSetupRepository()
        {
            ctx = FISEFDbContext.Instance;
        }

        public WorkflowTemplate CreateWorkflowTemplate(WorkflowTemplate workflowTemplate)
        {
            ctx.WorkflowTemplates.Add(workflowTemplate);
            ctx.SaveChanges();
            return workflowTemplate;
        }

        public WorkflowTemplate ReadWorkflowTemplate(int workflowTemplateId)
        {
           WorkflowTemplate workflowTemplate = ctx.WorkflowTemplates.Find(workflowTemplateId);
           ctx.Entry<WorkflowTemplate>(workflowTemplate).Collection<FileSpecification>(wt => wt.FileSpecifications).Load();
           workflowTemplate.FileSpecifications = workflowTemplate.FileSpecifications.OrderBy(f => f.StepNumberInWorkflowTemplate).ToList();
           return workflowTemplate;
        }

        public WorkflowTemplate ReadWorkflowTemplate(string name)
        {
            IEnumerable<WorkflowTemplate> workflowTemplates = ctx.WorkflowTemplates.Where(wt => wt.Name.Equals(name));

            if (workflowTemplates.Count() == 0) return null;

            return workflowTemplates.First();
        }

        public WorkflowTemplate ReadSelectedWorkflowTemplate()
        {
            IEnumerable<WorkflowTemplate> selectedWorkflowTemplates = ctx.WorkflowTemplates.Where(wt => wt.IsChosen);

            if (selectedWorkflowTemplates.Count() == 0) return null;

            return selectedWorkflowTemplates.First();
        }

        public ICollection<WorkflowTemplate> ReadWorkflowTemplates()
        {
            return ctx.WorkflowTemplates.ToList();
        }
        
        public WorkflowTemplate UpdateWorkflowTemplate(WorkflowTemplate workflowTemplate)
        {
            ctx.WorkflowTemplates.Attach(workflowTemplate);
            ctx.Entry(workflowTemplate).State = EntityState.Modified;
            ctx.SaveChanges();
            return workflowTemplate;
        }

        public WorkflowTemplate DeleteWorkflowTemplate(int workflowTemplateId)
        {
            throw new NotImplementedException();
        }
    }
}
