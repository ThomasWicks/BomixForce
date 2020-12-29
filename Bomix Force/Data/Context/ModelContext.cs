using Bomix_Force.Data.Entities;
using Bomix_Force.Data.EntityConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bomix_Force.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public virtual DbSet<Bomix_PedidoVenda> Bomix_PedidoVenda { get; set; }
        [NotMapped]
        public virtual DbSet<Bomix_PedidoVendaItem> Bomix_PedidoVendaItem { get; set; }
        [NotMapped]
        public virtual DbSet<Bomix_NotaFiscalVenda> Bomix_NotaFiscalVendas { get; set; }
        public virtual DbSet<Nonconformity> Nonconformity { get; set; }
        public virtual DbSet<Document> Document { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bomix_PedidoVendaItem>().HasNoKey();
            modelBuilder.Entity<Bomix_NotaFiscalVenda>().HasNoKey();
        }
    }
}
