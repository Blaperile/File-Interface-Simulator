using File_Interface_Simulator.Models;
using FIS.BL;
using FIS.BL.Domain.Setup;
using FIS.BL.Util.CSV;
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
            FieldSpecification fieldSpecification = specSetupManager.AddFieldSpecification(fieldspecificationViewModel.Name, fieldspecificationViewModel.Path, fieldspecificationViewModel.Version);
            if (fieldSpecification != null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            ViewBag.error = "Name combined with version must be unique";
            return View(fieldspecificationViewModel);
        }

        [HttpGet]
        public ActionResult UploadFileSpecification()
        {
            FileSpecificationViewModel model = new FileSpecificationViewModel();
            IEnumerable<FieldSpecification> fieldSpecifications = specSetupManager.GetFieldSpecificatons();
            IList<String> fieldSpecificationStrings = new List<String>();
            foreach (FieldSpecification fieldSpec in fieldSpecifications)
            {
                fieldSpecificationStrings.Add(fieldSpec.Name + " - " + fieldSpec.Version);
            }
            model.FieldSpecifications = fieldSpecificationStrings;
            return View("UploadFileSpecification", model);
        }

        [HttpPost]
        public ActionResult UploadFileSpecification(FileSpecificationViewModel model)
        {
            try
            {
                specSetupManager.AddFileSpecification(model.Name, model.Path, model.IsInput, model.InDirectoryPath, model.ArchiveDirectoryPath, model.ErrorDirectoryPath, model.OutDirectoryPath, model.Version, model.FieldSpecification);
                return RedirectToAction("Index", "Home");
            } catch (FileReadException ex)
            {
                ViewBag.Error = ex.Message;

                //TODO: manier vinden om dit uit model te laten onthouden?
                IEnumerable<FieldSpecification> fieldSpecifications = specSetupManager.GetFieldSpecificatons();
                IList<String> fieldSpecificationStrings = new List<String>();
                foreach (FieldSpecification fieldSpec in fieldSpecifications)
                {
                    fieldSpecificationStrings.Add(fieldSpec.Name + " - " + fieldSpec.Version);
                }
                model.FieldSpecifications = fieldSpecificationStrings;

                return View(model);
            }
        }
    }
}