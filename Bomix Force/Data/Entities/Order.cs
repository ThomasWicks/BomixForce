using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int NumeroPedido { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Entrega { get; set; }
        public string Status { get; set; }
        public int? CompanyId { get; set; }
        public virtual List<Item> Item { get; set; }
        public virtual Company Company { get; set; }
        public virtual List<Person> Person { get; set; }
        public virtual List<N_conformity> N_Conformities { get; set; }
    }
}
