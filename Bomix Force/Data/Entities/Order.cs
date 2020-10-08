using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bomix_Force.Data.Entities
{
    public class Order
    {
        [key]
        public int Id { get; set; }
        public int NumeroPedido { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Entrega { get; set; }
        public string Status { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Company Company { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual List<Item> Item { get; set; }
        public virtual List<Nonconformity> N_Conformities { get; set; }
    }
}
