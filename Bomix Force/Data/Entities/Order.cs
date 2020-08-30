using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bomix_Force.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int NumeroPedido { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Entrega { get; set; }
        public string Status { get; set; }
        [ForeignKey("Company")]
        public int? CompanyId { get; set; }
        [ForeignKey("Comercial")]
        public int? ComercialId { get; set; }
        public virtual List<Item> Item { get; set; }
        public virtual Company Company { get; set; }
        public virtual Comercial Comercial { get; set; }
        public virtual List<N_conformity> N_Conformities { get; set; }
    }
}
