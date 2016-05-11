using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class AnswerContentLine
    {
        public int AnswerContentLineId { get; set; }
        public AnswerContent AnswerContent { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
