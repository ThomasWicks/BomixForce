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
        public int Pedido { get; set; }
        public char TipoRegistro { get; set; }
        public string Status { get; set; }
        public DateTime Emissao { get; set; }
        public string TipoFrete { get; set; }
        public int Client_ID { get; set; }
        public int Loja { get; set; }
        public string Cliente { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public int Vendedor_FK { get; set; }
        public string Vendedor { get; set; }
        public string Gerente { get; set; }
        public DateTime CondPagamento { get; set; }
    }
}
