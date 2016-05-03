using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Operational
{
    public class Message
    {
        /*A message that is received from MECOMS or a message that will be given to MECOMS.*/

        public int MessageId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public MessageState MessageState { get; set; }
        public string ErrorDescription { get; set; }
        public ICollection<HeaderField> HeaderFields { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public FileSpecification FileSpecification { get; set; }
        public Workflow Workflow { get; set; }
        public ICollection<IElement> Elements { get; set; }
    }
}
