using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class FileSpecificationDetailViewModel
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime CreationDate { get; set; }
        public string InputOutput { get; set; }
        public string InFolder { get; set; }
        public string ArchiveFolder { get; set; }
        public string ErrorFolder { get; set;  }
        public string OutputFolder { get; set; }
        public ICollection<HeaderConditionDetailViewModel> HeaderConditions { get; set; }
        public ICollection<GroupConditionViewModel> GroupConditions { get; set; }
        public ICollection<FieldConditionViewModel> FieldConditions { get; set; }
    }
}