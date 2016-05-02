using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class MessageTransactionDetailViewModel
    {
        public ICollection<MessageGroupDetailViewModel> Groups { get; set; }
        public ICollection<MessageFieldDetailViewModel> Fields { get; set; }
    }
}