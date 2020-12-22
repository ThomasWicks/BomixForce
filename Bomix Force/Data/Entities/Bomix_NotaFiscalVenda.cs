using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Entities
{
    public class Bomix_NotaFiscalVenda
    {
        [key]
        public int ID { get; set; }
        public string Nota { get; set; }
        public DateTime Emissao { get; set; }
        public string TipoDocumento { get; set; }
        public string Cliente_ID { get; set; }
        public string Cliente { get; set; }
        public string Loja { get; set; }
        public string CidadeCod { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string UF { get; set; }
        public string SetorMercado { get; set; }
        public string Segmento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Contato { get; set; }
        //public string Vendedor_FK { get; set; }
        public string Vendedor { get; set; }
        public string Gerente { get; set; }
        public string Transportadora { get; set; }
        public string CondPagamento { get; set; }
        public int QtdParcelas { get; set; }
    }
}