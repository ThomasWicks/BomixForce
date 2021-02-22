using Microsoft.AspNetCore.Http;
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
        public string Nf { get; set; }
        public int ItemEnum { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Company")]
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
