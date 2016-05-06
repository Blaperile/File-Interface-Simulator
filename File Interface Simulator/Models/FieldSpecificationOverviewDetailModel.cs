using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class FieldSpecificationOverviewDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Version { get; set; }
        public string Path { get; set; }
    }
}