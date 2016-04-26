using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public abstract class Specification
    {
        /*Contains general specification information that both file and field specifications have*/

        public int SpecificationId { get; set; }
        public string Name { get; set; }
        public DateTime UploadDate { get; set; }
        public string Version { get; set; }
        public string Path { get; set; }

    }
}
