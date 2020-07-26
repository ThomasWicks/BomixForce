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

            builder.Property(u => u.Email)
              .HasColumnName("EMAIL")
              .IsRequired();


            builder.Property(u => u.Cpf)
              .HasColumnName("CPF")
              .IsRequired();

            builder.Property(u => u.Tel)
              .HasColumnName("TEL")
              .IsRequired();

            builder.Property(u => u.Company_Id)
              .HasColumnName("COMPANYID")
              .IsRequired();

            builder.HasOne(u => u.Order)
                .WithMany(t=>t.Person);
                

        }
    }
}
