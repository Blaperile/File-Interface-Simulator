using File_Interface_Simulator.Models;
using FIS.BL;
using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace File_Interface_Simulator.Controllers
{
    public class WorkflowTemplateSetupController : Controller
    {
        private readonly IWorkflowTemplateSetupManager workflowTemplateSetupManager = new WorkflowTemplateSetupManager();
        private readonly ISpecificationSetupManager specSetupManager = new SpecificationSetupManager();

        [HttpGet]
        public ActionResult CreateWorkflowTemplate()
        {
            WorkflowTemplateViewModel model = new WorkflowTemplateViewModel();
            return View("CreateWorkflowTemplate", model);
        }

        [HttpPost]
        public ActionResult CreateWorkflowTemplate(WorkflowTemplateViewModel model)
        {
            try
            {
                WorkflowTemplate workflowTemplate = workflowTemplateSetupManager.AddWorkflowTemplate(model.Name);
               
                return RedirectToAction("WorkflowTemplateDetail", "WorkflowTemplateSetup", new { workflowTemplateId = workflowTemplate.WorkflowTemplateId });
            } catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult WorkflowTemplateDetail(int workflowTemplateId = 1)
        {
            WorkflowTemplate workflowTemplate = workflowTemplateSetupManager.GetWorkflowTemplate(workflowTemplateId);
            WorkflowTemplateDetailViewModel model = new WorkflowTemplateDetailViewModel()
            {
                WorkflowTemplateId = workflowTemplateId,
                Name = workflowTemplate.Name,
                CreationDate = workflowTemplate.CreationDate,
            };

            if (workflowTemplate.IsChosen)
            {
                model.IsActive = "Yes";
            } else
            {
                model.IsActive = "No";
            }

            IList<WorkflowTemplateFileSpecificationDetailViewModel> fileSpecifications = new List<WorkflowTemplateFileSpecificationDetailViewModel>();

            foreach (FileSpecification fileSpecification in workflowTemplate.FileSpecifications)
            {
                WorkflowTemplateFileSpecificationDetailViewModel fileSpecificationModel = new WorkflowTemplateFileSpecificationDetailViewModel()
                {
                    Name = fileSpecification.Name,
                    Type = fileSpecification.IsInput? "Input" : "Output",
                    Version = fileSpecification.Version
                };

                fileSpecifications.Add(fileSpecificationModel);
            }

            model.CurrentFileSpecifications = fileSpecifications;

            IList<WorkflowTemplatePossibleFileSpecificationDetailViewModel> possibleFileSpecificationModels = new List<WorkflowTemplatePossibleFileSpecificationDetailViewModel>();
            IEnumerable<FileSpecification> possibleFileSpecifications = specSetupManager.GetFileSpecifications();

            foreach (FileSpecification possibleFileSpecification in possibleFileSpecifications)
            {
                if (!workflowTemplate.FileSpecifications.Contains(possibleFileSpecification))
                {
                    WorkflowTemplatePossibleFileSpecificationDetailViewModel possibleFileSpecificationModel = new WorkflowTemplatePossibleFileSpecificationDetailViewModel()
                    {
                        Name = possibleFileSpecification.Name,
                        Type = possibleFileSpecification.IsInput ? "Input" : "Output",
                        Version = possibleFileSpecification.Version
                    };

                    possibleFileSpecificationModels.Add(possibleFileSpecificationModel);
                }
            }

            model.PossibleFileSpecifications = possibleFileSpecificationModels;

            return View("WorkflowTemplateDetail", model);
        }

        [HttpPost]
        public ActionResult WorkflowTemplateDetail(WorkflowTemplateDetailViewModel model)
        {
            string[] newStep = model.NewStep.Split('-');
            string specificationName = newStep.ElementAt(0).Trim();
            string specificationVersion = newStep.ElementAt(2).Trim();
            workflowTemplateSetupManager.AddStepToWorkflowTemplate(model.WorkflowTemplateId, model.NewSequenceNumber, specificationName, specificationVersion);

            return RedirectToAction("WorkflowTemplateDetail", new { workflowTemplateId = model.WorkflowTemplateId });
        }
    }
}