using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Bomix_PedidoVendaItem
    {
        public int C6_Recno { get; set; }
        public string TipoRegistro { get; set; }
        public string Pedido_FK { get; set; }
        public string Sequencia { get; set; }
        public string Produto_ID { get; set; }
        public string Produto { get; set; }
        public string Arte_ID { get; set; }
        public string Arte { get; set; }
        public string Personalizacao { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double Valor { get; set; }
    }
}
