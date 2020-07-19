using Bomix_Force.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bomix_Force.Data.EntityConfig
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("ORDER");
            builder.HasKey(e => new { e.IdOrder });

            builder.Property(u => u.Number)
               .HasColumnName("NUMBER")
               .IsRequired();

            builder.Property(u => u.Date)
               .HasColumnName("DATE")
               .IsRequired();

            builder.Property(u => u.Status_Order)
               .HasColumnName("STATUS")
               .IsRequired();

            builder.Property(u => u.Person_id_request)
               .HasColumnName("PERSON_ID_REQUEST")
               .IsRequired();

            builder.Property(u => u.CompanyId)
               .HasColumnName("COMPANYID")
               .IsRequired();

            builder.Property(u => u.Person_id_seller)
               .HasColumnName("PERSON_ID_SELLER")
               .IsRequired();
        }
    }
}
