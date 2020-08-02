using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            UserLoginList = new HashSet<UserLogin>();
        }
        public int IdProfile { get; set; }
        public string Active { get; set; }
        public DateTime? LockoutEndDateUTC { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<UserLogin> UserLoginList { get; set; }
        public virtual Person Person { get; set; }
    }
}
