using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Order
    {
        public int IdOrder { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string Status_Order { get; set; }
        public int Person_id_request { get; set; }
        public int CompanyId { get; set; }
        public int Person_id_seller { get; set; }
    }
}
