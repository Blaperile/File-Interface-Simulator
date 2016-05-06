using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Operational
{
    public class Group
    {
        /*A collection of different fields and/or other groups within a message.*/

        public int GroupId { get; set; }
        public string GroupCode { get; set; }
        public string Level { get; set; }
        public string ErrorDescription { get; set; }
        public Group ParentGroup { get; set; }
        public GroupCondition GroupCondition { get; set; }
        public ICollection<Field> Fields { get; set; }
        public Transaction Transaction { get; set; }
        public int Sequence { get; set; }

    }
}
