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
    public class HomeController : Controller
    {
        private readonly IWorkflowTemplateSetupManager workflowTemplateSetupManager = new WorkflowTemplateSetupManager();

        [HttpGet]
        public ActionResult Index()
        {
            ICollection<WorkflowTemplate> workflowTemplates = workflowTemplateSetupManager.GetWorkflowTemplates();
            HomeViewModel model = new HomeViewModel()
            {
                WorkflowTemplates = new List<String>()
            };

            foreach (WorkflowTemplate workflowTemplate in workflowTemplates)
            {
                model.WorkflowTemplates.Add(workflowTemplate.Name);
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel model)
        {
            workflowTemplateSetupManager.SelectWorkflowTemplate(model.ChosenWorkflowTemplate);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Error(ErrorViewModel model)
        {
            return View("Error", model);
        }
    }
}