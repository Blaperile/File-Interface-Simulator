using File_Interface_Simulator.Models;
using FIS.BL;
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
    }
}