using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    interface IWorkflowTemplateSetupManager
    {
        WorkflowTemplate addStepToWorkflowTemplate(int workFlowTemplateId, int stepNumber, int specificationId);
        WorkflowTemplate removeStepFromWorkflowTemplate(int workflowTemplateId, int stepNumber);
        WorkflowTemplate addWorkflowTemplate(String name);
        WorkflowTemplate getWorkFlowTemplate(int workflowTemplateId);
        List<WorkflowTemplate> getWorkflowTemplates();
        WorkflowTemplate getSelectedWorkflowTemplate();
        WorkflowTemplate removeWorkflowTemplate(int workflowTemplateId);
        WorkflowTemplate selectWorkflowTemplate(int workflowTemplateId);

    }
}
