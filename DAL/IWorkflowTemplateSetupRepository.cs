using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.DAL
{
    public interface IWorkflowTemplateSetupRepository
    {
        WorkflowTemplate CreateWorkflowTemplate(WorkflowTemplate workflowTemplate);
        WorkflowTemplate ReadWorkflowTemplate(int workflowTemplateId);
        WorkflowTemplate ReadWorkflowTemplate(string name);
        WorkflowTemplate ReadSelectedWorkflowTemplate();
        IEnumerable<WorkflowTemplate> ReadWorkflowTemplates();
        WorkflowTemplate UpdateWorkflowTemplate(WorkflowTemplate workflowTemplate);
        WorkflowTemplate DeleteWorkflowTemplate(int workflowTemplateId);
    }
}
