using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class FileSpecificationViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsInput { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string Version { get; set; }

        public IEnumerable<String> FieldSpecifications { get; internal set; }

        [Required]
        public string FieldSpecification { get; set; }

        [Required]
        public string InDirectoryPath { get; set; }

        [Required]
        public string ArchiveDirectoryPath { get; set; }

        [Required]
        public string ErrorDirectoryPath { get; set; }

        [Required]
        public string OutDirectoryPath { get; set; }
    }
}