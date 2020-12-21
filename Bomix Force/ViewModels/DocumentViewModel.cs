using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.ViewModels
{
    public class DocumentViewModel
    {
        public string Type { get; set; }
        public string BucketType { get; set; }
        public string BucketLidType { get; set; }
        public string Debit { get; set; }
        public string Other { get; set; }
        public List<IFormFile> FilePath { get; set; }
    }
}
