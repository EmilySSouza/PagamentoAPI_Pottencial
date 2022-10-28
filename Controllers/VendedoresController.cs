using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PagamentoApi.Context;
using PagamentoApi.Models;

namespace PagamentoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedoresController : ControllerBase
    {
        private readonly PagamentoContext _VendedorContext;

        public VendedoresController(PagamentoContext VendedorContext)
        {
            _VendedorContext = VendedorContext;
        }

        [HttpGet("Visualizar_Vendedor_Por_Id{id}")]
        public IActionResult ObterVendedorPorId(int id)
        {
            var Vendedor = _VendedorContext.Vendedor.Find(id);

            if (Vendedor == null)
                return NotFound();

            return Ok(Vendedor);
        }

        [HttpPost("Adicionar_Um_Vendedor")]
        public IActionResult AdicionarVendedor(Vendedores Vendedor)
        {
            _VendedorContext.Add(Vendedor);
            _VendedorContext.SaveChanges();
            return CreatedAtAction(nameof(ObterVendedorPorId), new { id = Vendedor.Id }, Vendedor);
        }
    }
}