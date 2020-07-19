using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Permission
    {
        public Permission()
        {
            AccessList = new HashSet<Access>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? IdUser { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public virtual ICollection<Access> AccessList { get; set; }


        public bool Validate()
        {
            return true;
        }
    }
}
