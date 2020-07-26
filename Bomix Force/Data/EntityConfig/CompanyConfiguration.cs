using Bomix_Force.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bomix_Force.Data.EntityConfig
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("COMPANY");
            builder.HasKey(e => new { e.Id });

            builder.Property(u => u.Name)
               .HasColumnName("NAME")
               .IsRequired();

            builder.Property(u => u.Email)
              .HasColumnName("EMAIL")
              .IsRequired();

            builder.Property(u => u.Cnpj)
              .HasColumnName("CNPJ")
              .IsRequired();
        }
    }
}
