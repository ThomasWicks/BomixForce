using Bomix_Force.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bomix_Force.Data.EntityConfig
{
    public class N_conformityConfiguration : IEntityTypeConfiguration<Nonconformity>
    {
        public void Configure(EntityTypeBuilder<Nonconformity> builder)
        {
            builder.ToTable("N_CONFORMITY");
            builder.HasKey(e => new { e.Id });

            builder.Property(u => u.Lote)
              .HasColumnName("LOT")
              .IsRequired();

            builder.Property(u => u.Description)
             .HasColumnName("DESCRIPTION")
             .IsRequired();

            //builder.HasOne(u => u.)
            //    .WithMany(u => u.N_Conformities)
            //    .IsRequired();
                
        }
    }
}

