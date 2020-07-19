using Bomix_Force.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bomix_Force.Data.EntityConfig
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {

        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("PROFILE");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Active)
                .HasColumnName("ACTIVE")
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(p => p.Id)
                .HasColumnName("ID")
                .UseIdentityColumn();
        }
    }
}
