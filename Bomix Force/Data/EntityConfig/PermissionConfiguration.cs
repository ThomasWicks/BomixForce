using Bomix_Force.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bomix_Force.Data.EntityConfig
{
    public class PermissionConfiguration : EntityTypeConfiguration<Permission>
    {
        public PermissionConfiguration()
        {
            ToTable("PERMISSION");
            HasKey(p => p.Id);

            Property(p => p.ClaimType)
                .HasColumnName("CLAIMTYPE")
                .HasMaxLength(100);

            Property(p => p.ClaimValue)
                .HasColumnName("CLAIMVALUE")
                .HasMaxLength(100);

            Property(p => p.IdUser)
                .HasColumnName("ID_USER");

            Property(p => p.Id)
                .HasColumnName("ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
