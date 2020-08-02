using Bomix_Force.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.EntityConfig
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {

        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("USERLOGIN");
            builder.HasKey(ul => new { ul.IdUser, ul.LoginProvider, ul.ProviderKey });

            builder.Property(u => u.IdUser)
                .HasColumnName("ID_USER")
                .IsRequired();

            builder.Property(ul => ul.LoginProvider)
                .HasColumnName("LOGINPROVIDER")
                .HasMaxLength(128);

            builder.Property(ul => ul.ProviderKey)
                .HasColumnName("PROVIDERKEY")
                .HasMaxLength(128);
        }
    }
}
