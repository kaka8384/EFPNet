namespace EFPNet.Domain.Data.Migrations
{
    using EFPNet.Domains.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EfpDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EfpDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "test",
                Password = "123456",
                AddDate = DateTime.Now,
                Email = "test@126.com",
                LastUpdateDate = DateTime.Now,
                Mobile = "13768982283",
                NickName = "",
                RealName = ""
            };
            context.Users.AddOrUpdate(user);

        }
    }
}
