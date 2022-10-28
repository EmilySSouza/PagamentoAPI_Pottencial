using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagamentoApi.Models
{
    public class Compras
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public int IdVendedor { get; set; }

        public Vendas Venda { get; set; }
        public Vendedores Vendedor { get; set; }
    }
}