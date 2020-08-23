using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual List<Order> Order { get; set; }
    }
}
