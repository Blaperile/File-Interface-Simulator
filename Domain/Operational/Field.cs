using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Operational
{
    public class Field 
    { 
        /*Contains one particular item of data in a message, for example a customer name.*/

        public int FieldId { get; set; }
        public string FieldCode { get; set; }
        public string Value { get; set; }
        public string Level { get; set; }
        public string ErrorDescription { get; set; }
        public FileSpecFieldCondition FileSpecFieldCondition { get; set; }
        public Group Group { get; set; }
    }
}
