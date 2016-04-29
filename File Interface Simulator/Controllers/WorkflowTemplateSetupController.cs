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
                workflowTemplateSetupManager.AddWorkflowTemplate(model.Name);
                return View("Index", "Home");
            } catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult WorkflowTemplateDetail(int workflowTemplateId)
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
                    Name = fileSpecification.Name
                };

                if (fileSpecification.IsInput)
                {
                    fileSpecificationModel.Type = "Input";
                } else
                {
                    fileSpecificationModel.Type = "Output";
                }

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
                        Name = possibleFileSpecification.Name
                    };

                    if (possibleFileSpecification.IsInput)
                    {
                        possibleFileSpecificationModel.Type = "Input";
                    } else
                    {
                        possibleFileSpecificationModel.Type = "Output";
                    }

                    possibleFileSpecificationModels.Add(possibleFileSpecificationModel);
                }
            }

            model.PossibleFileSpecifications = possibleFileSpecificationModels;

            return View("WorkflowTemplateDetail", model);
        }

        [HttpPost]
        public ActionResult WorkflowTemplateDetail(WorkflowTemplateDetailViewModel model)
        {
            string specificationName = model.NewNameAndType.Split('-').ElementAt(0).Trim();
            workflowTemplateSetupManager.AddStepToWorkflowTemplate(model.WorkflowTemplateId, model.NewSequenceNumber, specificationName);

            return RedirectToAction("WorkflowTemplateDetail", new { workflowTemplateId = model.WorkflowTemplateId });
        }
    }
}