using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class FileSpecificationSearchViewModel
    {
        public string DocumentId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime CreationDate { get; set; }
        public string Type { get; set; }

        public ICollection<String> SearchOnPossibilities { get; set; }
        public string SearchOn { get; set; }
        public string DetailCode { get; set; }
        public string DetailName { get; set; }
        public string DetailDescription { get; set; }
    }
}