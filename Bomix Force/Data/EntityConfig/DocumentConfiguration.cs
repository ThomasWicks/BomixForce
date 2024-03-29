﻿using Bomix_Force.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bomix_Force.Data.EntityConfig
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("DOCUMENT");
            builder.HasKey(e => new { e.Id });

            builder.Property(u => u.Type)
               .HasColumnName("TYPE")
               .IsRequired();

            builder.Property(u => u.Date)
              .HasColumnName("DATE")
              .IsRequired();

            builder.Property(u => u.PathExtFile)
              .HasColumnName("PATH")
              .IsRequired();
        }
    }
}
