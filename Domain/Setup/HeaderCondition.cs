using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class HeaderCondition
    {
        /*Contains validation information for one specific type of header field*/

        public int HeaderFieldConditionId { get; set; }
        public string HeaderFieldCode { get; set; }
        public string Description { get; set; }
        public string Datatype { get; set; }
        public string Size { get; set; }
    }
}
