using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
        [ForeignKey("Comercial")]
        public int? ComercialId { get; set; }
        public virtual Comercial Comercial { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Person> Persons { get; set; }
    }
}
