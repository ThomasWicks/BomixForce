using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class User
    {
        public User()
        {
            UserLoginList = new HashSet<UserLogin>();
        }
        public int Id { get; set; }
        public int IdProfile { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Active { get; set; }
        public string Username { get; set; }
        public int IdEstablishment { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUTC { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool RecieveNotification { get; set; }
        public int AccessFailedCount { get; set; }
        public virtual Profile Profile { get; set; }
        //public virtual Establishment Establishment { get; set; }
        public virtual ICollection<UserLogin> UserLoginList { get; set; }
    }
}
