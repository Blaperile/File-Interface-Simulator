using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL
{
    class WorkflowTemplateSetupManager : IWorkflowTemplateSetupManager
    {
        public WorkflowTemplate AddStepToWorkflowTemplate(int workFlowTemplateId, int stepNumber, int specificationId)
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate AddWorkflowTemplate(string name)
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate GetSelectedWorkflowTemplate()
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate GetWorkFlowTemplate(int workflowTemplateId)
        {
            throw new NotImplementedException();
        }

        public List<WorkflowTemplate> GetWorkflowTemplates()
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate RemoveStepFromWorkflowTemplate(int workflowTemplateId, int stepNumber)
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate RemoveWorkflowTemplate(int workflowTemplateId)
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate SelectWorkflowTemplate(int workflowTemplateId)
        {
            throw new NotImplementedException();
        }
    }
}
