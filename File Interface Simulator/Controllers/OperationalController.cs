﻿using File_Interface_Simulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIS.BL;

namespace File_Interface_Simulator.Controllers
{
    public class OperationalController : Controller
    {
        private readonly ISpecificationSetupManager iSpecSetupManager = new SpecificationSetupManager(); 
        
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

            return RedirectToAction("Index","HomeController");
        }
    }
}