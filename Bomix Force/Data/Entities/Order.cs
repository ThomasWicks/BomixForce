using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string Status_Order { get; set; }
        public virtual List<Item> Item { get; set; }
        public int? CompanyId { get; set; }
        public int? PersonId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ApplicationUser Person { get; set; }
        public virtual List<N_conformity> N_Conformities { get; set; }
    }
}
