using Bomix_Force.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bomix_Force.Data.EntityConfig
{
    public class N_conformityConfiguration : IEntityTypeConfiguration<N_conformity>
    {
        public void Configure(EntityTypeBuilder<N_conformity> builder)
        {
            builder.ToTable("N_CONFORMITY");
            builder.HasKey(e => new { e.Id });

            builder.Property(u => u.Lot)
              .HasColumnName("LOT")
              .IsRequired();

            builder.Property(u => u.Description)
             .HasColumnName("DESCRIPTION")
             .IsRequired();

            builder.HasOne(u => u.Order)
                .WithMany(u => u.N_Conformities)
                .IsRequired();
                
        }
    }
}

