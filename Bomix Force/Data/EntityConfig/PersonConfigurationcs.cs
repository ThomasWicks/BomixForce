using Bomix_Force.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bomix_Force.Data.EntityConfig
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("PERSON");
            builder.HasKey(e => new { e.Id });

            builder.Property(u => u.Name)
               .HasColumnName("NAME")
               .IsRequired();
            builder.Property(u => u.Setor)
             .HasColumnName("SETOR")
             .IsRequired();

            builder.Property(u => u.Cargo)
             .HasColumnName("CARGO")
             .IsRequired();
        }
    }
}
