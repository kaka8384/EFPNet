using System;
using EFPNet.Domain.Data;
using EFPNet.Domains.Model;
using EFPNet.Domains.Repositories;

namespace EFPNet.Repositories
{
    internal class UserRepository : EFRepositoryBase<User, Guid>, IUserRepository
    {

        //public void AA()
        //{
        //    UnitOfWork.Rollback();
        //    var ss = "zhanghao";
        //}

        //public void BB()
        //{
        //    var container = DomainContainer.GetContainer();
        //    UnitOfWork = container.Resolve<IUnitOfWork>();
        //}
    }
}
