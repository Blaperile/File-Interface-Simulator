using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Operational
{
    public class Transaction
    {
        /*One distinct part of the message that contains all data for one transaction, for example for one new contract request*/

        public int TransactionId { get; set; }
        public string ErrorDescription { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public Message Message { get; set; }
    }
}
