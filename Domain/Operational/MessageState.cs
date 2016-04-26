using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Operational
{
    public enum MessageState
    {
        /*The current state of the message*/

        Uploaded, /*Message has been received and saved*/
        Validated, /*Message has been validated*/
        Processed,
        Created, /*Message has been generated*/
        Exported /*Message has been given to MECOMS*/
    }
}
