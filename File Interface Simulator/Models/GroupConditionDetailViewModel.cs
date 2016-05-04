using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class GroupConditionDetailViewModel
    {
        public GroupConditionViewModel GroupCondition { get; set; }
        public ICollection<FieldConditionViewModel> FieldConditions { get; set; }
    }
}