using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class N_conformity
    {
        public int Id { get; set; }
        public string Lot { get; set; }
        public string Description { get; set; }
        public int Id_Order { get; set; }
        public virtual Order Order { get; set; }
    }
}
