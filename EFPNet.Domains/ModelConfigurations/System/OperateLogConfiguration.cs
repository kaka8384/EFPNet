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
    public class OperateLogConfiguration : EntityTypeConfiguration<OperateLog>
    {
        public OperateLogConfiguration()
        {
            //主键
            HasKey(a => a.Id);

            Property(a => a.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.OperateDate)
                .IsRequired();
            Property(c => c.ActionName)
                .IsRequired();
            Property(c => c.ControllerName)
                .IsRequired();
            Property(c => c.Event).IsRequired();
            //用户和操作日志一对多
            HasRequired(a => a.OperateUser)
            .WithMany(b => b.OperateLogs)
            .HasForeignKey(a => a.UserID).WillCascadeOnDelete(false);
        }
    }
}
