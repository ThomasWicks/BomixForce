using Bomix_Force.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.EntityConfig
{
    public class AccessConfiguration : EntityTypeConfiguration<Access>
    {
        public AccessConfiguration()
        {
            ToTable("ACCESS");
            HasKey(e => new { e.IdProfile, e.IdPermission });


            Property(u => u.IdProfile)
                .HasColumnName("ID_PROFILE")
                .IsRequired();

            Property(u => u.IdPermission)
                .HasColumnName("ID_PERMISSION")
                .IsRequired();

            Property(u => u.IdUser)
                .HasColumnName("ID_USER");

            HasRequired(e => e.Profile)
                .WithMany(t => t.AccessList)
                .HasForeignKey(e => e.IdProfile)
                .WillCascadeOnDelete(false);

            HasRequired(e => e.Permission)
                .WithMany(t => t.AccessList)
                .HasForeignKey(e => e.IdPermission)
                .WillCascadeOnDelete(false);
        }

    }
}
