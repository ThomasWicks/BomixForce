using Bomix_Force.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int Pedido { get; set; }
        public string Status { get; set; }
        public DateTime Emissao { get; set; }
        public string Cliente { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }

        public virtual List<Bomix_PedidoVendaItem> Item { get; set; }

        //public int Id { get; set; }
        //public int Pedido { get; set; }
        //public DateTime Emissao { get; set; }
        //public DateTime Entrega { get; set; }
        //public string Status { get; set; }
        //public virtual List<Item> Item { get; set; }
    }
}
