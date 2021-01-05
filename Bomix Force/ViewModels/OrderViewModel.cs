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
        public string OrdemCompra { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PCP { get; set; }

        public List<OrderViewModel> orderViewModelDetails;
    }
}
