using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EFPNet.Domains.Model;

namespace EFPNet.Domains.ModelConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            //主键
            HasKey(a => a.Id);

            Property(a => a.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.UserName)
                .IsRequired()
                .HasMaxLength(20);
            Property(c => c.Password)
                .IsRequired()
                .HasMaxLength(20);
            Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(50);
            Property(a => a.Mobile)
                .IsRequired()
                .HasMaxLength(11);
            Property(a => a.RealName)
                .HasMaxLength(30);
            Property(a => a.NickName)
        .HasMaxLength(30);
            //ToTable("User");
        }
    }
}
