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
            builder.HasKey(e => new { e.Id });

            builder.Property(u => u.Number)
               .HasColumnName("NUMBER")
               .IsRequired();

            builder.Property(u => u.Date)
               .HasColumnName("DATE")
               .IsRequired();

            builder.Property(u => u.Status_Order)
               .HasColumnName("STATUS")
               .IsRequired();

            builder.HasOne(e => e.Company)
                 .WithMany(t => t.Orders).IsRequired();

            builder.HasMany(e => e.Item)
                .WithOne(t => t.Order).IsRequired() ;

        }
    }
}
