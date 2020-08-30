using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Comercial
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Setor { get; set; }
        public string Cargo { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual List<Company> Companies { get; set; }
    }
}
