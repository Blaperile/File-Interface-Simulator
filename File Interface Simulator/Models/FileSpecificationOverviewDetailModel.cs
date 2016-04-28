using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class FileSpecificationOverviewDetailModel
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Version { get; set; }
        public string InputOutput { get; set; }
        public string Path { get; set; }
    }
}