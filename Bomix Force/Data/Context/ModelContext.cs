using Bomix_Force.Data.Entities;
using Bomix_Force.Data.EntityConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Bomix_Force.Data.Context
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options)
           : base(options)
        {
        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Access> Access { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<N_conformity> N_conformity { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Document> Document { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            ////modelBuilder.HasDefaultSchema(ContextConfig.SchemeName);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new AccessConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new N_conformityConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            
        }
    }
}
