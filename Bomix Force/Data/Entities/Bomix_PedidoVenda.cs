using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Bomix_PedidoVenda
    {
        [key]
        public int id { get; set; }
        public string Pedido { get; set; }
        public string TipoRegistro { get; set; }
        public string Status { get; set; }
        public DateTime Emissao { get; set; }
        public string TipoFrete { get; set; }
        public string Cliente_ID { get; set; }
        public string Loja { get; set; }
        public string Cliente { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Vendedor_FK { get; set; }
        public string Vendedor { get; set; }
        public string Gerente { get; set; }
        public string CondPagamento { get; set; }
    }
}
