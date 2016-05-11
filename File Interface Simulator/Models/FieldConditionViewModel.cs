using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class FieldConditionViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Optional { get; set; }
        public int Values { get; set; }
        public string Datatype { get; set; }
        public int Size { get; set; }
        public string Format { get; set;  }
        public string  Group { get; set; }
        public string Level { get; set; }
        public int GroupConditionId { get; set; }
    }
}