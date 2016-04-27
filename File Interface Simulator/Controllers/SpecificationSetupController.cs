using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace File_Interface_Simulator.Controllers
{
    public class SpecificationSetupController : Controller
    {
        // GET: SpecificationSetup
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadFieldSpecification()
        {

            return View();
        }
    }
}