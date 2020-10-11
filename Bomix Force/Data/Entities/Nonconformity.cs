using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Nonconformity
    {
        [Key]
        public int Id { get; set; }
        public string Lote { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        //[ForeignKey("Order")]
        //public int? OrderId { get; set; }
        //public virtual Order Order { get; set; }
    }
}
