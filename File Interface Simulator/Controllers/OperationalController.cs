using File_Interface_Simulator.Models;
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
        [HttpGet]
        public ActionResult MessageDetail(int id = 1)
        {
            MessageDetailViewModel model = new MessageDetailViewModel()
            {
                HeaderFields = new List<MessageHeaderFieldDetailViewModel>(),
                Transactions = new List<MessageTransactionDetailViewModel>()
            };
            return View("MessageDetail", model);
        }
    }
}