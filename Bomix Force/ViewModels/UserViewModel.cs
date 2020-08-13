using Bomix_Force.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Tel { get; set; }
        public  Company Company { get; set; }
    }
}
