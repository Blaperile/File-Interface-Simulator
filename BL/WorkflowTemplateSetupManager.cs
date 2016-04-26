using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class WorkflowTemplateSetupManager : IWorkflowTemplateSetupManager
    {
        public WorkflowTemplate addStepToWorkflowTemplate(int workFlowTemplateId, int stepNumber, int specificationId)
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate addWorkflowTemplate(string name)
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate getSelectedWorkflowTemplate()
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate getWorkFlowTemplate(int workflowTemplateId)
        {
            throw new NotImplementedException();
        }

        public List<WorkflowTemplate> getWorkflowTemplates()
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate removeStepFromWorkflowTemplate(int workflowTemplateId, int stepNumber)
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate removeWorkflowTemplate(int workflowTemplateId)
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate selectWorkflowTemplate(int workflowTemplateId)
        {
            throw new NotImplementedException();
        }
    }
}
