using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class GroupDetailViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Range { get; set; }
        public string Level { get; set; }
        public string Transactie { get; set; }
        public string ErrorMessage { get; set; }
        public int AmountOfErrorMessages { get; set; }
        public int AmountOfFields { get; set;  }
        public ICollection<MessageFieldDetailViewModel> Fields { get; set; }
    }
}