using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Company
    {
        public int IdCompany { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
    }
}
