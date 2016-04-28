﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class FileSpecificationViewModel
    {
        public string Name { get; set; }
        public bool IsInput { get; set; } = true;
        public string Path { get; set; }
        public string Version { get; set; }
        public IEnumerable<String> FieldSpecifications { get; internal set; }
        public string FieldSpecification { get; set; }
        public string InDirectoryPath { get; set; }
        public string ArchiveDirectoryPath { get; set; }
        public string ErrorDirectoryPath { get; set; }
        public string OutDirectoryPath { get; set; }
    }
}