using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class MessageDetailViewModel
    {
        public string MessageId { get; set; }
        public string CreationDate { get; set; }
        public string MessageState { get; set; }
        public string Type { get; set; }
        public string SpecificationFile { get; set; }
        public int AmountOfHeaderErrors { get; set; }
        public int AmountOfErrors { get; set; }
        public ICollection<MessageHeaderFieldDetailViewModel> HeaderFields { get; set; }
        public ICollection<MessageTransactionDetailViewModel> Transactions { get; set; }
    }
}