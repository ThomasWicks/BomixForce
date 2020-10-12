using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bomix_Force.ViewModels
{
    public class NonconformityViewModel
    {
        public string Lote { get; set; }
        public string Description { get; set; }
        public string Nf { get; set; }
        public int ItemEnum { get; set; }
        public int Quantity { get; set; }
        public string Answer { get; set; }
    }
}
