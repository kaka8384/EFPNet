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
    public class MenuConfiguration : EntityTypeConfiguration<Menu>
    {
        public MenuConfiguration()
        {
            //主键
            HasKey(a => a.Id);
            Property(a => a.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.MenuName)
                .IsRequired().HasMaxLength(20);
            Property(c => c.ActionName).IsRequired();
            Property(c => c.ControllerName).IsRequired();
            Property(c => c.IsPage).IsRequired();
            Property(c => c.IsShow).IsRequired();
            Property(c => c.Sort).IsRequired();
        }
    }
}
