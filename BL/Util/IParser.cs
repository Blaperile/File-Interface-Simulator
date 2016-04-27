using FIS.BL.Domain.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util
{
    public interface IParser
    {
        IEnumerable<IElement> GetElements();
    }
}
