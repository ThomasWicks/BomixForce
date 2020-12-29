using Bomix_Force.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.ViewModels
{
    public class OrderViewModel
    {
        public int id { get; set; }
        public string Pedido { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] 
        public DateTime Emissao { get; set; }
        public string Cliente { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public string Arte { get; set; }
        public string Personalizacao { get; set; }
        public float Valor { get; set; }
        public float OrdemCompra { get; set; }

        public List<OrderViewModel> orderViewModelDetails;

        //public virtual List<Bomix_PedidoVendaItem> Item { get; set; }

        //public int Id { get; set; }
        //public int Pedido { get; set; }
        //public DateTime Emissao { get; set; }
        //public DateTime Entrega { get; set; }
        //public string Status { get; set; }
        //public virtual List<Item> Item { get; set; }
    }
}
