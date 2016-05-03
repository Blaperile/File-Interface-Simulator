using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class MessageTransactionDetailViewModel
    {
        public int AmountOfErrors { get { return AmountOfGroupErrors + AmountOfFieldErrors; } }

        public int AmountOfGroupErrors { get; set; }
        public ICollection<MessageGroupDetailViewModel> Groups { get; set; }

        public int AmountOfFieldErrors { get; set; }
        public ICollection<MessageFieldDetailViewModel> Fields { get; set; }
    }
}