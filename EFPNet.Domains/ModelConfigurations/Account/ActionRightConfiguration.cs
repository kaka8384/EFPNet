using EFPNet.Domains.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPNet.Domains.ModelConfigurations
{
    public class ActionRightConfiguration : EntityTypeConfiguration<ActionRight>
    {
        public ActionRightConfiguration()
        {
            //主键
            HasKey(a => a.Id);
            Property(a => a.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.ActionName)
           .IsRequired();
            Property(c => c.ActionType)
            .IsRequired();
            Property(c => c.ControllerName)
            .IsRequired();
            Property(c => c.Sort)
            .IsRequired();
            //角色和权限菜单多对多
            HasMany(a => a.Roles)
                .WithMany(b => b.ActionRights)
                .Map(r =>
                {
                    r.ToTable("RoleActionRight");
                    r.MapLeftKey("RoleID");
                    r.MapRightKey("ActionRightID");
                });
        }
    }
}
