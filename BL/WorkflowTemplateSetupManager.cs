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
            fileSpecification.StepNumberInWorkflowTemplate = stepNumber;

            foreach(FileSpecification workflowTemplateFileSpecification in workflowTemplate.FileSpecifications.Where(fs => fs.StepNumberInWorkflowTemplate >= stepNumber))
            {
                workflowTemplateFileSpecification.StepNumberInWorkflowTemplate++;
            }

            workflowTemplate.FileSpecifications.Add(fileSpecification);
            fileSpecification.WorkflowTemplate = workflowTemplate;
            return workflowTemplateSetupRepo.UpdateWorkflowTemplate(workflowTemplate);
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

        public WorkflowTemplate RemoveStepFromWorkflowTemplate(int workflowTemplateId, int stepNumber)
        {
            throw new NotImplementedException();
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

            WorkflowTemplate newSelectedWorkflowTemplate = GetWorkflowTemplate(workflowTemplateId);
            newSelectedWorkflowTemplate.IsChosen = true;
            return workflowTemplateSetupRepo.UpdateWorkflowTemplate(newSelectedWorkflowTemplate);
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
    }
}
