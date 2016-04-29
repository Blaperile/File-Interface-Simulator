using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class MessageTransactionDetailViewModel
    {
        public IEnumerable<MessageGroupDetailViewModel> Groups { get; set; }
        public IEnumerable<MessageFieldDetailViewModel> Fields { get; set; }
    }
}