using System;
using EFPNet.Domains.Model;
using EFPNet.Infrastructure.Data;

namespace EFPNet.Domains.Repositories
{
    public interface IRoleRepository : IRepository<Role, Guid>
    {
    }
}
