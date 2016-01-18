using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPNet.Infrastructure.Tools
{
    public enum OperateLogEvent
    {
        ActionExecuting = 0,
        ActionExecuted = 1,
        ResultExecuting = 2,
        ResultExecuted = 3
    }
}
