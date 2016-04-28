using FIS.BL.Domain.Setup;
using FIS.BL.Exceptions;
using FIS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL
{
    public class WorkflowTemplateSetupManager : IWorkflowTemplateSetupManager
    {
        private IWorkflowTemplateSetupRepository workflowTemplateSetupRepo;

        public WorkflowTemplateSetupManager()
        {
            workflowTemplateSetupRepo = new WorkflowTemplateSetupRepository();
        }

        public WorkflowTemplate AddStepToWorkflowTemplate(int workFlowTemplateId, int stepNumber, int specificationId)
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate AddWorkflowTemplate(string name)
        {
            WorkflowTemplate workflowTemplate = GetWorkflowTemplate(name);

            if (workflowTemplate == null)
            {
                workflowTemplate = new WorkflowTemplate()
                {
                    Name = name,
                    CreationDate = DateTime.Now,
                    IsChosen = false
                };

                return workflowTemplateSetupRepo.CreateWorkflowTemplate(workflowTemplate);
            } else
            {
                throw new WorkflowTemplateSetupException("A workflow with the name " + name + " already exists.");
            }
        }

        public WorkflowTemplate GetSelectedWorkflowTemplate()
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate GetWorkflowTemplate(int workflowTemplateId)
        {
            throw new NotImplementedException();
        }

        public WorkflowTemplate GetWorkflowTemplate(string name)
        {
            return workflowTemplateSetupRepo.ReadWorkflowTemplate(name);
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
