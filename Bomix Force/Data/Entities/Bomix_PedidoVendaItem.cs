using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Bomix_PedidoVendaItem
    {
        public int C6_Recno { get; set; }
        public char TipoRegistro { get; set; }
        public int Pedido_FK { get; set; }
        public int Sequencia { get; set; }
        public string Produto_ID { get; set; }
        public string Produto { get; set; }
        public int Arte_ID { get; set; }
        public string Arte { get; set; }
        public string Personalizacao { get; set; }
        public int Quantidade { get; set; }
        public float ValorUnitario { get; set; }
        public float Valor { get; set; }
    }
}
