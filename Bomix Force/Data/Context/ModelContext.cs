using Bomix_Force.Data.Entities;
using Bomix_Force.Data.EntityConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bomix_Force.ViewModels;


namespace Bomix_Force.Data.Context
{
    public class ModelContext : IdentityDbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
           : base(options)
        {
        }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Employee_Seller> Employee_Seller { get; set; }
        public virtual DbSet<Bomix_PedidoVenda> Bomix_PedidoVenda { get; set; }
        public virtual DbSet<Bomix_PedidoVendaItem> Bomix_PedidoVendaItem { get; set; }

        //public virtual DbSet<Order> Order { get; set; }
        //public virtual DbSet<N_conformity> N_conformity { get; set; }
        //public virtual DbSet<Item> Item { get; set; }
        //public virtual DbSet<Document> Document { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bomix_PedidoVendaItem>().HasNoKey();
            //modelBuilder.ApplyConfiguration(new OrderConfiguration());
            //modelBuilder.ApplyConfiguration(new N_conformityConfiguration());
            //modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            //modelBuilder.ApplyConfiguration(new ItemConfiguration());
            //modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            //modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}
