using System;
using System.Collections.Generic;
using System.Text;
using Bomix_Force.Data.Entities;
using Bomix_Force.Data.EntityConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bomix_Force.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Access> Access { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.HasDefaultSchema(ContextConfig.SchemeName);
            modelBuilder.Entity<User>(b => new UserConfiguration());
            modelBuilder.Entity<Profile>(b => new ProfileConfiguration());
            modelBuilder.Entity<Access>(b => new AccessConfiguration());
            modelBuilder.Entity<Permission>(b => new PermissionConfiguration());
            modelBuilder.Entity<UserLogin>(b => new UserLoginConfiguration());
        }
    }
}
