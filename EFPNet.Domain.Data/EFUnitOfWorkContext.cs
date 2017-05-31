using System.Configuration;
using System.Data.Entity;
using Autofac;
using EFPNet.Infrastructure.Data;
using EFPNet.Infrastructure.Tools.Extensions;
using EFPNet.Infrastructure.Tools.Ioc;

namespace EFPNet.Domain.Data
{
    ///// <summary>
    /////     EF数据单元操作类
    ///// </summary>
    public class EFUnitOfWorkContext : UnitOfWorkContextBase
    {

        public IDbContext dbContext { get; set; }

        /// <summary>
        ///     获取 当前使用的数据访问上下文对象
        /// </summary>
        protected override DbContext Context
        {
            get
            {
                return DomainContainer.GetContainer().ResolveNamed<DbContext>("EF");
            }
        }

        //[Import("EF", typeof(DbContext))]
        //private DbContext EFDbContext
        //{
        //    get
        //    {
        //        return DomainContainer.GetContainer().ResolveNamed<DbContext>("EF");
        //    }
        //}

        //[Import("EFCaching", typeof(DbContext))]
        //private DbContext EFCachingDbContext
        //{
        //    get
        //    {
        //        return DomainContainer.GetContainer().ResolveNamed<DbContext>("EFCaching");
        //    }
        //}

    }
}
