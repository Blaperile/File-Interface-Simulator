using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util
{
    public interface IAnswerGenerator
    {
        Message GenerateAnswer(Message message, FileSpecification fileSpecification);
    }
}
