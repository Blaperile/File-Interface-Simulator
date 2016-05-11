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
        public ActionResult WorkflowTemplateDetail(int id = 1, string error = "")
        {
            ViewBag.error = error;

            try { 
            WorkflowTemplate workflowTemplate = workflowTemplateSetupManager.GetWorkflowTemplate(id);

                WorkflowTemplateDetailViewModel model = new WorkflowTemplateDetailViewModel()
                {
                    WorkflowTemplateId = id,
                    Name = workflowTemplate.Name,
                    CreationDate = workflowTemplate.CreationDate,
                };

                if (workflowTemplate.IsChosen)
                {
                    model.IsActive = "Yes";
                }
                else
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
                        Type = workflowTemplateStep.fileSpecification.IsInput ? "Input" : "Output",
                        Version = workflowTemplateStep.fileSpecification.Version,
                        Id = workflowTemplateStep.WorkflowTemplateStepId,
                        FileSpecificationId = workflowTemplateStep.fileSpecification.FileSpecificationId
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
            } catch(Exception e)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    ErrorMessage = e.Message
                };

                return RedirectToAction("Error", "Home", model);
            }
        }

        [HttpPost]
        public ActionResult WorkflowTemplateDetail(WorkflowTemplateDetailViewModel model)
        {
            if (!String.IsNullOrEmpty(model.NewStep))
            {
                string[] newStep = model.NewStep.Split('-');
                string specificationName = newStep.ElementAt(0).Trim();
                string specificationVersion = newStep.ElementAt(2).Trim();
                workflowTemplateSetupManager.AddStepToWorkflowTemplate(model.WorkflowTemplateId, model.NewSequenceNumber, specificationName, specificationVersion);

                return RedirectToAction("WorkflowTemplateDetail", new { workflowTemplateId = model.WorkflowTemplateId });
            } else
            {
                return RedirectToAction("WorkflowTemplateDetail", new { workflowTemplateId = model.WorkflowTemplateId, error = "You must select a file specification when you add a new step." });
            }
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
            try
            {
                WorkflowTemplate workflowTemplate = workflowTemplateSetupManager.RemoveWorkflowTemplate(id);
                return new HttpStatusCodeResult(200, "Succes");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        public HttpStatusCodeResult RemoveStepFromWorkflowTemplateRPC(int workflowTemplateStepId, int workflowTemplateId) {
            try
            {
                WorkflowTemplateStep workflowTemplateStep = workflowTemplateSetupManager.RemoveStepFromWorkflowTemplate(workflowTemplateStepId, workflowTemplateId);
                return new HttpStatusCodeResult(200, "Succes");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

    }
}