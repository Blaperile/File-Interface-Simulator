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
           ctx.Entry<WorkflowTemplate>(workflowTemplate).Collection<WorkflowTemplateStep>(wt => wt.WorkflowTemplateSteps).Load();
           workflowTemplate.WorkflowTemplateSteps = workflowTemplate.WorkflowTemplateSteps.OrderBy(f => f.StepNumber).ToList();
           foreach(WorkflowTemplateStep workflowTemplateStep in workflowTemplate.WorkflowTemplateSteps)
            {
                ctx.Entry<WorkflowTemplateStep>(workflowTemplateStep).Reference<FileSpecification>(wts => wts.fileSpecification).Load();
            }
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
            WorkflowTemplate workflowTemplate = ctx.WorkflowTemplates.Find(workflowTemplateId);
            workflowTemplate.WorkflowTemplateSteps = null;
            List<WorkflowTemplateStep> workflowTemplateSteps = ctx.WorkflowTemplateSteps.Where(f => f.WorkflowTemplate.WorkflowTemplateId == workflowTemplateId).ToList();
            foreach (WorkflowTemplateStep workflowTemplateStep in workflowTemplateSteps) {
                workflowTemplateStep.WorkflowTemplate = null;
                ctx.WorkflowTemplateSteps.Attach(workflowTemplateStep);
                ctx.Entry(workflowTemplateStep).State = EntityState.Modified;
            }
            ctx.WorkflowTemplates.Remove(workflowTemplate);
            ctx.SaveChanges();
            return workflowTemplate;
        }

        public WorkflowTemplateStep CreateWorkflowTemplateStep(WorkflowTemplateStep workflowTemplateStep)
        {
            ctx.WorkflowTemplateSteps.Add(workflowTemplateStep);
            ctx.SaveChanges();
            return workflowTemplateStep;
        }

        public ICollection<WorkflowTemplateStep> ReadWorkflowTemplateSteps(int workflowTemplateId)
        {
            return ctx.WorkflowTemplateSteps.Where(wt => wt.WorkflowTemplate.WorkflowTemplateId == workflowTemplateId).ToList();
        }
    }
}
