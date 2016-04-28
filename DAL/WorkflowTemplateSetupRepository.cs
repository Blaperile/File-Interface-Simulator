using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIS.BL.Domain.Setup;
using FIS.DAL.EF;

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
            throw new NotImplementedException();
        }

        public WorkflowTemplate ReadWorkflowTemplate(string name)
        {
            IEnumerable<WorkflowTemplate> workflowTemplates = ctx.WorkflowTemplates.Where(wt => wt.Name.Equals(name));

            if (workflowTemplates.Count() == 0) return null;

            return workflowTemplates.First();
        }

        public WorkflowTemplate ReadSelectedWorkflowTemplate()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkflowTemplate> ReadWorkflowTemplates()
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate UpdateWorkflowTemplate(WorkflowTemplate workflowTemplate)
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate DeleteWorkflowTemplate(int workflowTemplateId)
        {
            throw new NotImplementedException();
        }
    }
}
