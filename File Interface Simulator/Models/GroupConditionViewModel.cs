using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class GroupConditionViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Range { get; set; }
        public int AmountFields { get; set; }
        public string Level { get; set; }
    }
}