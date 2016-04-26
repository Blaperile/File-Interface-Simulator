using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL
{
    interface IWorkflowTemplateSetupManager
    {
        WorkflowTemplate AddStepToWorkflowTemplate(int workFlowTemplateId, int stepNumber, int specificationId);
        WorkflowTemplate RemoveStepFromWorkflowTemplate(int workflowTemplateId, int stepNumber);
        WorkflowTemplate AddWorkflowTemplate(String name);
        WorkflowTemplate GetWorkFlowTemplate(int workflowTemplateId);
        List<WorkflowTemplate> GetWorkflowTemplates();
        WorkflowTemplate GetSelectedWorkflowTemplate();
        WorkflowTemplate RemoveWorkflowTemplate(int workflowTemplateId);
        WorkflowTemplate SelectWorkflowTemplate(int workflowTemplateId);

    }
}
