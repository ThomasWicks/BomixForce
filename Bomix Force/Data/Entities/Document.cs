using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
