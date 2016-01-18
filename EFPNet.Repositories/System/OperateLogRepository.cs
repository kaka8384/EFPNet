using EFPNet.Domain.Data;
using EFPNet.Domains.Model;
using EFPNet.Domains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPNet.Repositories
{
    internal class OperateLogRepository : EFRepositoryBase<OperateLog, Guid>, IOperateLogRepository
    {

    }
}
