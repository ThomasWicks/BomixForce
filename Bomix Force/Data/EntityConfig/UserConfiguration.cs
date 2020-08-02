using Bomix_Force.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bomix_Force.Data.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.IdProfile)
                .HasColumnName("ID_PROFILE")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(u => u.Active)
                .HasColumnName("ACTIVE")
                .HasColumnType("CHAR")
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(u => u.EmailConfirmed)
                .HasColumnName("EMAILCONFIRMED")
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasColumnName("PASSWORDHASH")
                .IsRequired();

            builder.Property(u => u.SecurityStamp)
                .HasColumnName("SECURITYSTAMP");

            builder.Property(u => u.PhoneNumber)
                .HasColumnName("PHONENUMBER")
                .HasMaxLength(50);

            builder.Property(u => u.PhoneNumberConfirmed)
                .HasColumnName("PHONENUMBERCONFIRMED")
                //.HasColumnType("NUMBER")
                .IsRequired();

            builder.Property(u => u.TwoFactorEnabled)
                .HasColumnName("TWOFACTORENABLED")
                // .HasColumnType("NUMBER")
                .IsRequired();

            builder.Property(u => u.LockoutEndDateUTC)
                .HasColumnName("LOCKOUTENDDATEUTC");
            //.HasColumnType("TIMESTAMP")
            //.HasPrecision(6);

            builder.Property(u => u.LockoutEnabled)
                .HasColumnName("LOCKOUTENABLED")
                // .HasColumnType("NUMBER")
                .IsRequired();

            builder.Property(u => u.AccessFailedCount)
                .HasColumnName("ACCESSFAILEDCOUNT")
                .IsRequired()
                .HasColumnType("int");

            builder.HasOne(u => u.Profile)
                .WithMany(p => p.UserList)
                .HasForeignKey(u => u.IdProfile)
                .IsRequired();

            //HasRequired(u => u.Establishment)
            //    .WithMany(e => e.Users)
            //    .HasForeignKey(u => u.IdEstablishment)
            //    .WillCascadeOnDelete(false);

            builder.HasOne(u => u.Person)
                .WithOne(t => t.User)
                .HasForeignKey<Person>(i => i.UserId)
                .IsRequired();
        }
    }
}
