using File_Interface_Simulator.Models;
using FIS.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace File_Interface_Simulator.Controllers
{
    public class SpecificationSetupController : Controller
    {
        private readonly ISpecificationSetupManager specSetupManager = new SpecificationSetupManager();

        // GET: Operational
        [HttpGet]
        public ActionResult UploadFieldSpecification()
        {
            var fieldSpecificationViewModel = new FieldSpecificationViewModel();
            return View("UploadFieldSpecification", fieldSpecificationViewModel);
        }

        [HttpPost]
        public ActionResult UploadFieldSpecification(FieldSpecificationViewModel fieldspecificationViewModel) //save entered data
        {
            specSetupManager.AddFieldSpecification(fieldspecificationViewModel.Name, fieldspecificationViewModel.Path, fieldspecificationViewModel.Version);
            return RedirectToAction("Index", "HomeController");
        }
    }
}