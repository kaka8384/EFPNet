using EFPNet.Domains.Model;
using EFPNet.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPNet.Domains.Repositories
{
    public interface IMenuRepository : IRepository<Menu, Guid>
    {
    }
}
