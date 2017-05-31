using System.Data.Entity;
using EFPNet.Domains.Model;
using EFPNet.Domains.ModelConfigurations;
using EFPNet.Infrastructure.Data;

namespace EFPNet.Domain.Data
{
    /// <summary>
    /// 表示专用于EFPNet的数据访问上下文。
    /// </summary>
    public class EfpDbContext : DbContext, IDbContext
    {
        #region 构造函数
        /// <summary>
        /// 构造函数，初始化一个新的<c>EfpDbContext</c>实例。
        /// </summary>
        public EfpDbContext()
            : base("Name=EfpDbContext")
        {
        }
        #endregion

        #region 公共属性
        public DbSet<User> Users { set; get; }
        public DbSet<Role> Role { set; get; }
        public DbSet<ActionRight> ActionRight { set; get; }
        public DbSet<OperateLog> OperateLog { set; get; }
        public DbSet<Menu> Menu { set; get; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new ActionRightConfiguration());
            modelBuilder.Configurations.Add(new OperateLogConfiguration());
            modelBuilder.Configurations.Add(new MenuConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
