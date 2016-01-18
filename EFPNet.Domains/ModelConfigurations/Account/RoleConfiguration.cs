using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using EFPNet.Domains.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFPNet.Domains.ModelConfigurations
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            //主键
            HasKey(a => a.Id);

            Property(a => a.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.RoleName)
                .IsRequired()
                .HasMaxLength(20);
            Property(a => a.RoleDesc)
                .HasMaxLength(100);
            //用户和角色多对多
            HasMany(a=>a.Users)
                .WithMany(b=>b.Roles)
                .Map(r =>
                {
                    r.ToTable("UserRole");
                    r.MapLeftKey("UserID");
                    r.MapRightKey("RoleID");
                });
        }
    }
}
