using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagamentoApi.Models
{
    public class Vendas
    {
        public int Id { get; set; }
        public int IdVendedor { get; set; }
        public DateTime Data { get; set; }
        public string Itens { get; set; }
        public EnumStatusVenda StatusVenda { get; set; }
    }
}