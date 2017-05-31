using System;
using EFPNet.Domain.Data;
using EFPNet.Domains.Model;
using EFPNet.Domains.Repositories;

namespace EFPNet.Repositories
{
    internal class UserRepository : EFRepositoryBase<User, Guid>, IUserRepository
    {
    }
}
