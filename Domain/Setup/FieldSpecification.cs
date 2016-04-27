﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class FieldSpecification
    {
        /*Represents the Field Specification document, a long document containing detailed validation information about fields*/
        public int FieldSpecificationId { get; set; }
        public string Name { get; set; }
        public DateTime UploadDate { get; set; }
        public string Version { get; set; }
        public string Path { get; set; }
        public IEnumerable<FieldSpecFieldCondition> FieldSpecFieldConditions { get; set; }
    }
}