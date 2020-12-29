using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bomix_Force.Data.Entities
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public DateTime Date { get; set; }
        public string PathExtFile { get; set; }
    }
}
