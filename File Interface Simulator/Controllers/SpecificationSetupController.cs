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
            return RedirectToAction("Index","HomeController");
        }

        [HttpGet]
        public ActionResult UploadFileSpecification()
        {
            FileSpecificationViewModel model = new FileSpecificationViewModel();
            model.FieldSpecificationVersions = specSetupManager.GetFieldSpecificationVersions();
            return View("UploadFileSpecification", model);
        }

        [HttpPost]
        public ActionResult UploadFileSpecification(FileSpecificationViewModel model)
        {
            specSetupManager.AddFileSpecification(model.Name, model.Path, model.IsInput, model.InDirectoryPath, model.ArchiveDirectoryPath, model.ErrorDirectoryPath, model.OutDirectoryPath, model.FieldSpecificationVersion);
            return RedirectToAction("Index", "HomeController");
        }
    }
}