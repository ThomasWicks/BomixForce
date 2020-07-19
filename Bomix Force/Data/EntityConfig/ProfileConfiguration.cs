using Bomix_Force.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bomix_Force.Data.EntityConfig
{
    public class ProfileConfiguration : EntityTypeConfiguration<Profile>
    {
        public ProfileConfiguration()
        {
            ToTable("PROFILE");
            HasKey(p => p.Id);

            Property(p => p.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.Active)
                .HasColumnName("ACTIVE")
                .IsRequired()
                .HasMaxLength(1);

            Property(p => p.Id)
                .HasColumnName("ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
