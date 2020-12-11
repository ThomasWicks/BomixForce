using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.ViewModels
{
    public class FinancialViewModel
    {
        public string Nota { get; set; }
        public string Emissao { get; set; }
        public string Pedido { get; set; }
        public string Sequencia { get; set; }
        public string Item { get; set; }
        public string Lote { get; set; }
        public DateTime DataValidade { get; set; }
        public string TipoProduto { get; set; }
        public string Produto_ID { get; set; }
        public string Produto { get; set; }
        public string Arte { get; set; }
        public string Personalizacao { get; set; }
        public string Cor { get; set; }
        public int Quantidade { get; set; }
        public float ValorLiquido { get; set; }
        public float ValorBruto { get; set; }
        public float ValorUnitario { get; set; }

    }
}
