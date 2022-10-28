using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PagamentoApi.Models;

namespace PagamentoApi.Context
{
    public class PagamentoContext : DbContext
    {
        public PagamentoContext(DbContextOptions<PagamentoContext> options) : base (options)
        {

        }

        public DbSet<Vendas> Vendas { get; set;}
        public DbSet<Vendedores> Vendedor { get; set; }
        public DbSet<Compras> Compra { get; set; }

    }
}