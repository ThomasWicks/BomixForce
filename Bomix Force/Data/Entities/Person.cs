﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Setor { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public virtual Company Company { get; set; }
        [NotMapped]
        public virtual List<Order> Order { get; set; }
    }
}
