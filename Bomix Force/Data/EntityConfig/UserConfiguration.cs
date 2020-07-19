using Bomix_Force.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bomix_Force.Data.EntityConfig
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("USER");
            HasKey(u => u.Id);

            Property(u => u.IdProfile)
                .HasColumnName("ID_PROFILE")
                .IsRequired();

            Property(u => u.IdEstablishment)
                .HasColumnName("ID_ESTABLISHMENT")
                .IsRequired();

            Property(u => u.Cpf)
                .HasColumnName("CPF")
                .HasMaxLength(11)
                .IsRequired();

            Property(u => u.Name)
                .HasColumnName("NAME")
                .HasMaxLength(50)
                .IsRequired();

            Property(u => u.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(50)
                .IsRequired();

            Property(u => u.Active)
                .HasColumnName("ACTIVE")
                .HasColumnType("CHAR")
                .IsRequired()
                .HasMaxLength(1);

            Property(u => u.Username)
                .HasColumnName("USERNAME")
                .HasMaxLength(50)
                .IsRequired();

            Property(u => u.EmailConfirmed)
                .HasColumnName("EMAILCONFIRMED")
                .IsRequired();

            Property(u => u.PasswordHash)
                .HasColumnName("PASSWORDHASH")
                .IsRequired();

            Property(u => u.SecurityStamp)
                .HasColumnName("SECURITYSTAMP");

            Property(u => u.PhoneNumber)
                .HasColumnName("PHONENUMBER")
                .HasMaxLength(50);

            Property(u => u.PhoneNumberConfirmed)
                .HasColumnName("PHONENUMBERCONFIRMED")
                //.HasColumnType("NUMBER")
                .IsRequired();

            Property(u => u.TwoFactorEnabled)
                .HasColumnName("TWOFACTORENABLED")
                // .HasColumnType("NUMBER")
                .IsRequired();

            Property(u => u.LockoutEndDateUTC)
                .HasColumnName("LOCKOUTENDDATEUTC");
            //.HasColumnType("TIMESTAMP")
            //.HasPrecision(6);

            Property(u => u.LockoutEnabled)
                .HasColumnName("LOCKOUTENABLED")
                // .HasColumnType("NUMBER")
                .IsRequired();

            Property(u => u.RecieveNotification)
                .HasColumnName("RECIEVENOTIFICATION")
                // .HasColumnType("NUMBER")
                .IsRequired();

            Property(u => u.AccessFailedCount)
                .HasColumnName("ACCESSFAILEDCOUNT")
                .IsRequired()
                .HasColumnType("int");

            HasRequired(u => u.Profile)
                .WithMany(p => p.UserList)
                .HasForeignKey(u => u.IdProfile)
                .WillCascadeOnDelete(false);

            //HasRequired(u => u.Establishment)
            //    .WithMany(e => e.Users)
            //    .HasForeignKey(u => u.IdEstablishment)
            //    .WillCascadeOnDelete(false);

            Property(u => u.Id)
                .HasColumnName("ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
