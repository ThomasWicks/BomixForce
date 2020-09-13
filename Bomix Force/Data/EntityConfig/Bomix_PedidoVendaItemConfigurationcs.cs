using Bomix_Force.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.EntityConfig
{
    public class Bomix_PedidoVendaItemConfiguration : IEntityTypeConfiguration<Bomix_PedidoVendaItem>
    {
        public void Configure(EntityTypeBuilder<Bomix_PedidoVendaItem> builder)
        {
            builder.ToTable("BOMIX_PEDIDOVENDA");
            builder.HasKey(e => new { e.C6_Recno });
            builder.Property(u => u.TipoRegistro)
               .HasColumnName("TipoRegistro")
               .IsRequired();
            builder.Property(u => u.Pedido_FK)
              .HasColumnName("Pedido_FK")
              .IsRequired();
            builder.Property(u => u.Sequencia)
              .HasColumnName("Sequencia")
              .IsRequired();
            builder.Property(u => u.Produto_ID)
              .HasColumnName("Produto_ID")
              .IsRequired();
            builder.Property(u => u.Produto)
              .HasColumnName("Produto")
              .IsRequired();
            builder.Property(u => u.Arte_ID)
              .HasColumnName("Arte_ID")
              .IsRequired();
            builder.Property(u => u.Arte)
              .HasColumnName("Arte")
              .IsRequired();
            builder.Property(u => u.Personalizacao)
              .HasColumnName("Personalizacao")
              .IsRequired();
            builder.Property(u => u.Quantidade)
              .HasColumnName("Quantidade")
              .IsRequired();
            builder.Property(u => u.ValorUnitario)
              .HasColumnName("ValorUnitario")
              .IsRequired();
            builder.Property(u => u.Valor)
              .HasColumnName("Valor")
              .IsRequired();
        }
    }
}
