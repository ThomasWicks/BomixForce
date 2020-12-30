using Bomix_Force.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.EntityConfig
{
    public class Bomix_PedidoVendaConfiguration : IEntityTypeConfiguration<Bomix_PedidoVenda>
    {
        public void Configure(EntityTypeBuilder<Bomix_PedidoVenda> builder)
        {
            builder.ToTable("BOMIX_PEDIDOVENDA");
            builder.HasKey(e => new { e.id });

            builder.Property(u => u.Pedido)
               .HasColumnName("Pedido")
               .IsRequired();

            builder.Property(u => u.TipoRegistro)
              .HasColumnName("TipoRegistro")
              .IsRequired();

            builder.Property(u => u.Status)
             .HasColumnName("Status")
             .IsRequired();

            builder.Property(u => u.Emissao)
           .HasColumnName("Emissao")
           .IsRequired();

            builder.Property(u => u.TipoFrete)
          .HasColumnName("TipoFrete")
          .IsRequired();

            builder.Property(u => u.Cliente_ID)
          .HasColumnName("Client_ID")
          .IsRequired();

            builder.Property(u => u.Loja)
         .HasColumnName("Loja")
         .IsRequired();

            builder.Property(u => u.Cliente)
     .HasColumnName("Cliente")
     .IsRequired();
            builder.Property(u => u.Cidade)
     .HasColumnName("Cidade")
     .IsRequired();
            builder.Property(u => u.UF)
     .HasColumnName("UF")
     .IsRequired();
            builder.Property(u => u.Vendedor_FK)
     .HasColumnName("Vendedor_FK")
     .IsRequired();
            builder.Property(u => u.Vendedor)
     .HasColumnName("Vendedor")
     .IsRequired();
            builder.Property(u => u.CondPagamento)
     .HasColumnName("CondPagamento")
     .IsRequired();
            builder.Property(u => u.OrdemCompra)
     .HasColumnName("OrdemCompra");
        }



    }
}
