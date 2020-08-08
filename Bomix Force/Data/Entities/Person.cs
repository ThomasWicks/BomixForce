using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Cpf { get; set; }
        public int Tel { get; set; }
        public string UserId { get; set; }
        public virtual Company Company { get; set; }
    }
}
