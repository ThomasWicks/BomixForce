//using Bomix_Force.Data.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Bomix_Force.Data.EntityConfig
//{
//    public class AccessConfiguration : IEntityTypeConfiguration<Access>
//    {
//        public void Configure(EntityTypeBuilder<Access> builder)
//        {
//            builder.ToTable("ACCESS");
//            builder.HasKey(e => new { e.IdProfile, e.IdPermission });


//            builder.Property(u => u.IdProfile)
//                .HasColumnName("ID_PROFILE")
//                .IsRequired();

//            builder.Property(u => u.IdPermission)
//                .HasColumnName("ID_PERMISSION")
//                .IsRequired();

//            builder.Property(u => u.IdUser)
//                .HasColumnName("ID_USER");

//            builder.HasOne(e => e.Profile)
//                .WithMany(t => t.AccessList)
//                .HasForeignKey(e => e.IdProfile)
//                .IsRequired();

//            builder.HasOne(e => e.Permission)
//                .WithMany(t => t.AccessList)
//                .HasForeignKey(e => e.IdPermission)
//                .IsRequired();
//        }
//    }
//}
