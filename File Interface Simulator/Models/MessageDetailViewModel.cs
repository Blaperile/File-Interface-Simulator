using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class MessageDetailViewModel
    {
        public string MessageId { get; set; }
        public DateTime CreationDate { get; set; }
        public string MessageState { get; set; }
        public string Type { get; set; }
        public string SpecificationFile { get; set; }
        public IEnumerable<MessageHeaderFieldDetailViewModel> HeaderFields { get; set; }
        public IEnumerable<MessageTransactionDetailViewModel> Transactions { get; set; }
    }
}