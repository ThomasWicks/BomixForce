using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
    }
}
