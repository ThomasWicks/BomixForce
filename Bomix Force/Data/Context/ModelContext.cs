using Bomix_Force.Data.Entities;
using Bomix_Force.Data.EntityConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Context
{
    public class ModelContext : DbContext
    {
        public ModelContext()
           : base("Mysqlconnection")
        {
        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Access> Access { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.HasDefaultSchema(ContextConfig.SchemeName);
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new ProfileConfiguration());
            modelBuilder.Configurations.Add(new AccessConfiguration());
            modelBuilder.Configurations.Add(new PermissionConfiguration());
            modelBuilder.Configurations.Add(new UserLoginConfiguration());
        }
    }
}
