using System;
using EFPNet.Domains.Model;
using EFPNet.Infrastructure.Data;

namespace EFPNet.Domains.Repositories
{
    public interface IUserRepository : IRepository<User,Guid>
    {
    }
}
