using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Profile
    {
        public Profile()
        {
            UserList = new HashSet<User>();
            AccessList = new HashSet<Access>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Active { get; set; }
        public virtual ICollection<User> UserList { get; set; }
        public virtual ICollection<Access> AccessList { get; set; }
    }
}
