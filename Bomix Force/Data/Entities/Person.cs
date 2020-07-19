using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Person
    {
        public int IdPerson { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Sector { get; set; }
        public int Cpf { get; set; }
        public int Tel { get; set; }
        public int Company_Id { get; set; }
    }
}
