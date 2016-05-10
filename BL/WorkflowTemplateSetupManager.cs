using FIS.BL.Domain.Operational;
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
        private ISpecificationSetupManager specSetupManager;
        private IOperationalManager operationalManager;
        private IWorkflowTemplateSetupRepository workflowTemplateSetupRepo;

        public WorkflowTemplateSetupManager()
        {
            specSetupManager = new SpecificationSetupManager();
            workflowTemplateSetupRepo = new WorkflowTemplateSetupRepository();
        }

        public WorkflowTemplate AddStepToWorkflowTemplate(int workflowTemplateId, int stepNumber, string specificationName, string specificationVersion)
        {
             WorkflowTemplate workflowTemplate = GetWorkflowTemplate(workflowTemplateId);
             FileSpecification fileSpecification = specSetupManager.GetFileSpecification(specificationName, specificationVersion);
             WorkflowTemplateStep workflowTemplateStep = new WorkflowTemplateStep()
            {
                StepNumber = stepNumber,
                WorkflowTemplate = workflowTemplate,
                fileSpecification = fileSpecification
            };


             foreach(WorkflowTemplateStep workflowTemplateStepInWorkflow in workflowTemplate.WorkflowTemplateSteps.Where(wts => wts.StepNumber >= stepNumber))
             {
                 workflowTemplateStepInWorkflow.StepNumber++;
             }

            workflowTemplateStep = workflowTemplateSetupRepo.CreateWorkflowTemplateStep(workflowTemplateStep); 
            if(fileSpecification.WorkflowTemplateSteps == null)
            {
                fileSpecification.WorkflowTemplateSteps = new List<WorkflowTemplateStep>();
            }
            if(workflowTemplate.WorkflowTemplateSteps == null)
            {
                workflowTemplate.WorkflowTemplateSteps = new List<WorkflowTemplateStep>();
            }
            return workflowTemplate;

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
                    IsChosen = false,
                    Workflows = new List<Workflow>()
                };

                return workflowTemplateSetupRepo.CreateWorkflowTemplate(workflowTemplate);
            } else
            {
                throw new WorkflowTemplateSetupException("A workflow with the name " + name + " already exists.");
            }
        }

        public WorkflowTemplate GetSelectedWorkflowTemplate()
        {
            return workflowTemplateSetupRepo.ReadSelectedWorkflowTemplate();
        }

        public WorkflowTemplate GetWorkflowTemplate(int workflowTemplateId)
        {
            return workflowTemplateSetupRepo.ReadWorkflowTemplate(workflowTemplateId);
        }

        public WorkflowTemplate GetWorkflowTemplate(string name)
        {
            return workflowTemplateSetupRepo.ReadWorkflowTemplate(name);
        }

        public ICollection<WorkflowTemplate> GetWorkflowTemplates()
        {
            return workflowTemplateSetupRepo.ReadWorkflowTemplates();
        }

        public WorkflowTemplateStep RemoveStepFromWorkflowTemplate(int workflowTemplateStepId, int workflowTemplateId)
        {
            try
            {
                List<Workflow> workflows = operationalManager.GetWorkflowsForTemplate(workflowTemplateId);
                return null;
            }
            catch
            {
                WorkflowTemplate workflowTemplate = workflowTemplateSetupRepo.ReadWorkflowTemplate(workflowTemplateId);
                foreach (WorkflowTemplateStep workflowTemplateStepInWorkflow in workflowTemplate.WorkflowTemplateSteps.Where(wts => wts.StepNumber >= workflowTemplate.WorkflowTemplateSteps.Where(wfts=>wfts.WorkflowTemplateStepId == workflowTemplateStepId).FirstOrDefault().StepNumber))
                {
                    workflowTemplateStepInWorkflow.StepNumber--;
                }
                return workflowTemplateSetupRepo.DeleteWorkflowTemplateStep(workflowTemplateStepId);
            }
        }

        public WorkflowTemplate RemoveWorkflowTemplate(int workflowTemplateId)
        {
            try
            {
                List<Workflow> workflows = operationalManager.GetWorkflowsForTemplate(workflowTemplateId);
                return null;
            }
            catch
            {
                return workflowTemplateSetupRepo.DeleteWorkflowTemplate(workflowTemplateId);
            }
        }

        public WorkflowTemplate SelectWorkflowTemplate(int workflowTemplateId)
        {
            WorkflowTemplate currentlySelectedWorkflowTemplate = GetSelectedWorkflowTemplate();

            if (currentlySelectedWorkflowTemplate != null)
            {
                currentlySelectedWorkflowTemplate.IsChosen = false;
                currentlySelectedWorkflowTemplate = workflowTemplateSetupRepo.UpdateWorkflowTemplate(currentlySelectedWorkflowTemplate);
            }

            if (currentlySelectedWorkflowTemplate == null || currentlySelectedWorkflowTemplate.WorkflowTemplateId != workflowTemplateId)
            {
                WorkflowTemplate newSelectedWorkflowTemplate = GetWorkflowTemplate(workflowTemplateId);
                newSelectedWorkflowTemplate.IsChosen = true;
                return workflowTemplateSetupRepo.UpdateWorkflowTemplate(newSelectedWorkflowTemplate);
            } else
            {
                return currentlySelectedWorkflowTemplate;
            }
        }

        public WorkflowTemplate SelectWorkflowTemplate(string name)
        {
            WorkflowTemplate currentlySelectedWorkflowTemplate = GetSelectedWorkflowTemplate();

            if (currentlySelectedWorkflowTemplate != null)
            {
                currentlySelectedWorkflowTemplate.IsChosen = false;
                currentlySelectedWorkflowTemplate = workflowTemplateSetupRepo.UpdateWorkflowTemplate(currentlySelectedWorkflowTemplate);
            }

            WorkflowTemplate newSelectedWorkflowTemplate = GetWorkflowTemplate(name);
            newSelectedWorkflowTemplate.IsChosen = true;
            return workflowTemplateSetupRepo.UpdateWorkflowTemplate(newSelectedWorkflowTemplate);
        }

        public WorkflowTemplateStep GetWorkflowTemplateStep(int workflowTemplateId, int stepNumber)
        {
            return workflowTemplateSetupRepo.ReadWorkflowTemplateStep(workflowTemplateId, stepNumber);
        }
    }
}
