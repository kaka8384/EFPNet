using System.Data.Entity;
using EFPNet.Domain.Data.Migrations;

namespace EFPNet.Domain.Data.Initialize
{
    public static class DatabaseInitializer
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EfpDbContext, Configuration>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfpDbContext>());
            //using (var db = new EfpDbContext())
            //{
            //    db.Database.Initialize(true);
            //}
        }
    }
}
