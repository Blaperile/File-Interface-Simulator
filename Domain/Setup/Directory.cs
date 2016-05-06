using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class Directory
    {
        /*Represents a directory on the hard drive. This keeps the information for where messages need to be read from and sent to.*/
        public int DirectoryId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        [Required]
        public FileSpecification FileSpecification { get; set; }
    }
}
