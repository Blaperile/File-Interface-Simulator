using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Operational
{
    public class HeaderField
    {
        /*A field with data that is located in the header of a message*/

        public int HeaderFieldId { get; set; }
        public string HeaderFieldCode { get; set; }
        public string Description { get; set; }
        public string ErrorDescription { get; set; }
        public HeaderCondition HeaderCondition { get; set; }
    }
}
