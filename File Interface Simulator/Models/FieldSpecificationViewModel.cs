using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class FieldSpecificationViewModel
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Path { get; set; }
        [Required]
        public String Version { get; set; }
    }
}