using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL
{
    public interface IWorkflowTemplateSetupManager
    {
        WorkflowTemplate AddStepToWorkflowTemplate(int workflowTemplateId, int stepNumber, string specificationName, string specificationVersion);
        WorkflowTemplateStep RemoveStepFromWorkflowTemplate(int workflowTemplateStepId, int workflowTemplateId);
        WorkflowTemplate AddWorkflowTemplate(String name);
        WorkflowTemplate GetWorkflowTemplate(int workflowTemplateId);
        WorkflowTemplate GetWorkflowTemplate(string name);
        ICollection<WorkflowTemplate> GetWorkflowTemplates();
        WorkflowTemplate GetSelectedWorkflowTemplate();
        WorkflowTemplate RemoveWorkflowTemplate(int workflowTemplateId);
        WorkflowTemplate SelectWorkflowTemplate(int workflowTemplateId);
        WorkflowTemplate SelectWorkflowTemplate(string name);

        WorkflowTemplateStep GetWorkflowTemplateStep(int workflowTemplateId, int stepNumber);
    }
}
