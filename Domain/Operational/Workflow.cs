using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Operational
{
    public class Workflow
    {
        /*One run of a particular WorkflowTemplate that contains all messages sent and received during this run of the workflow.*/

        public int WorkflowId { get; set; }
        public DateTime Date { get; set; }
        public bool IsSuccessful { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public WorkflowTemplate WorkflowTemplate { get; set; }
        
    }
}
