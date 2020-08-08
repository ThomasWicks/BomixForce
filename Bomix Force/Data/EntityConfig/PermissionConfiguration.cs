//using Bomix_Force.Data.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Bomix_Force.Data.EntityConfig
//{
//    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
//    {
//        public void Configure(EntityTypeBuilder<Permission> builder)
//        {
//            builder.ToTable("PERMISSION");
//            builder.HasKey(p => p.Id);

//            builder.Property(p => p.ClaimType)
//                .HasColumnName("CLAIMTYPE")
//                .HasMaxLength(100);

//            builder.Property(p => p.ClaimValue)
//                .HasColumnName("CLAIMVALUE")
//                .HasMaxLength(100);

//            builder.Property(p => p.IdUser)
//                .HasColumnName("ID_USER");

//            builder.Property(p => p.Id)
//                .HasColumnName("ID")
//                .UseIdentityColumn();
//        }
//    }
//}
