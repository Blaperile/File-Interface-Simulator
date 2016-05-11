using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class AnswerContentViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        public IEnumerable<String> FileSpecifications { get; internal set; }
        [Required]
        public string FileSpecification { get; set; }
    }
}