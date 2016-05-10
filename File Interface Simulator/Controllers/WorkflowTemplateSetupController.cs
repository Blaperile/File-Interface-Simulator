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

            IList<WorkflowTemplateStepDetailViewModel> workflowTemplateSteps = new List<WorkflowTemplateStepDetailViewModel>();

            foreach (WorkflowTemplateStep workflowTemplateStep in workflowTemplate.WorkflowTemplateSteps)
            {
                WorkflowTemplateStepDetailViewModel workflowTemplateStepModel = new WorkflowTemplateStepDetailViewModel()
                {
                    Step = workflowTemplateStep.StepNumber,
                    Name = workflowTemplateStep.fileSpecification.Name,
                    Type = workflowTemplateStep.fileSpecification.IsInput? "Input" : "Output",
                    Version = workflowTemplateStep.fileSpecification.Version,
                    Id = workflowTemplateStep.WorkflowTemplateStepId
                };

                workflowTemplateSteps.Add(workflowTemplateStepModel);
            }

            model.CurrentWorkflowTemplateSteps = workflowTemplateSteps;

            IList<WorkflowTemplatePossibleFileSpecificationDetailViewModel> possibleFileSpecificationModels = new List<WorkflowTemplatePossibleFileSpecificationDetailViewModel>();
            IEnumerable<FileSpecification> possibleFileSpecifications = specSetupManager.GetFileSpecifications();

            foreach (FileSpecification possibleFileSpecification in possibleFileSpecifications)
            {
                if (workflowTemplate.WorkflowTemplateSteps.Count != 0)
                {
                    foreach (WorkflowTemplateStep workflowtemplateStep in workflowTemplate.WorkflowTemplateSteps)
                    {
                        if (!workflowtemplateStep.fileSpecification.Equals(possibleFileSpecification))
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
                }
                else
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

        [HttpGet]
        public ActionResult WorkflowTemplateOverview()
        {
            IEnumerable<WorkflowTemplate> workflowTemplates = workflowTemplateSetupManager.GetWorkflowTemplates();

            ICollection<WorkflowTemplateOverviewDetailViewModel> model = new List<WorkflowTemplateOverviewDetailViewModel>();

            foreach (WorkflowTemplate workflowTemplate in workflowTemplates)
            {
                model.Add(new WorkflowTemplateOverviewDetailViewModel()
                {
                    Id = workflowTemplate.WorkflowTemplateId,
                    Name = workflowTemplate.Name,
                    CreationDate = workflowTemplate.CreationDate,
                    IsActive = workflowTemplate.IsChosen
                });
            }

            return View("WorkflowTemplateOverview", model);
        }

        public ActionResult SelectWorkflowTemplateRPC(int id)
        {
            WorkflowTemplate workflowTemplate = workflowTemplateSetupManager.SelectWorkflowTemplate(id);
            return RedirectToAction("WorkflowTemplateOverview");
        }

        public HttpStatusCodeResult RemoveWorkflowTemplateRPC(int id)
        {
            WorkflowTemplate workflowTemplate = workflowTemplateSetupManager.RemoveWorkflowTemplate(id);

            if (workflowTemplate != null)
            {
                return new HttpStatusCodeResult(200, "Succes");
            }

            return new HttpStatusCodeResult(500, "Workflowtemplate can not be deleted because workflows already exist.");
        }

        public HttpStatusCodeResult RemoveStepFromWorkflowTemplateRPC(int workflowTemplateStepId, int workflowTemplateId) {
            WorkflowTemplateStep workflowTemplateStep = workflowTemplateSetupManager.RemoveStepFromWorkflowTemplate(workflowTemplateStepId, workflowTemplateId);
            if(workflowTemplateStep != null)
            {
                return new HttpStatusCodeResult(200, "Succes");
            }
            return new HttpStatusCodeResult(500, "Workflowtemplatestep can not be deleted because workflows already exist");
        }

    }
}