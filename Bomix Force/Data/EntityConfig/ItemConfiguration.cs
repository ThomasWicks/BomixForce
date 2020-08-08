using Bomix_Force.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bomix_Force.Data.EntityConfig
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("ITEM");
            builder.HasKey(e => new { e.Id });

            builder.Property(u => u.Status_art)
             .HasColumnName("STATUS_ART")
             .IsRequired();

            builder.Property(u => u.Description)
             .HasColumnName("DESCRIPTION")
             .IsRequired();
        }
    }
}
