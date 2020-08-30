using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class N_conformity
    {
        [Key]
        public int Id { get; set; }
        public string Lot { get; set; }
        public string Description { get; set; }
        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
