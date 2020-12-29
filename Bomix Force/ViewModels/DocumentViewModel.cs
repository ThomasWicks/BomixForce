using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.ViewModels
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public string IdentityUserId { get; set; }
        public bool AF { get; set; }
        public bool AVCB { get; set; }
        public bool AS { get; set; }
        public bool LA { get; set; }
        public bool CISSO9001 { get; set; }
        public bool CFSSC22000 { get; set; }
        public bool LMG { get; set; }
        public bool LMB { get; set; }
        public bool ET { get; set; }
        public bool CND { get; set; }
        public bool DAA { get; set; }
        public bool OT { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string BucketType { get; set; }
        public string BucketLidType { get; set; }
        public string Debit { get; set; }
        public string Other { get; set; }
        public string PathExtFile { get; set; }
        public List<IFormFile> FilePath { get; set; }
    }
}
