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
    public class VendasController : ControllerBase
    {
        private readonly PagamentoContext _VendasContext;

        public VendasController(PagamentoContext VendasContext)
        {
            _VendasContext = VendasContext;
        }

        [HttpPost("Adicionar_Uma_Venda")]
        public IActionResult AdicionarVenda(Vendas Venda)
        {
            var Vendedor = _VendasContext.Vendedor.Find(Venda.IdVendedor);

            if (Vendedor == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(Venda.Itens))
            {
                return BadRequest(new { Erro = "O item não pode estar vazio" });
            }

            Venda.StatusVenda = EnumStatusVenda.Aguardando_Pagamento;
            _VendasContext.Vendas.Add(Venda);
            _VendasContext.SaveChanges();
            return CreatedAtAction(nameof(ObterVendaPorId), new { id = Venda.Id }, Venda);

        }

        [HttpPut("Atualizar_A_Venda{id}")]
        public IActionResult AtualizarVenda(int id, Vendas Venda, EnumStatusVenda status)
        {
            var VendaBanco = _VendasContext.Vendas.Find(id);

            if(VendaBanco == null)
                return NotFound();

            if(Venda.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da compra não pode estar vazia" });

            VendaBanco.Data = Venda.Data;
            VendaBanco.IdVendedor = Venda.IdVendedor;
            VendaBanco.Itens = Venda.Itens;
            
            if(Venda.StatusVenda == EnumStatusVenda.Aguardando_Pagamento)
            {
                if(status != EnumStatusVenda.Pagamento_Aprovado && status != EnumStatusVenda.Cancelada)
                {
                    return BadRequest(new { Erro = "Atualização invalida. Verifique se o status da venda que quer atualizar está correto."});
                }
            } 

            if(Venda.StatusVenda == EnumStatusVenda.Pagamento_Aprovado)
            {
                if(status != EnumStatusVenda.Enviado_para_Transportadora && status != EnumStatusVenda.Cancelada)
                {
                    return BadRequest(new { Erro = "Atualização invalida. Verifique se o status da venda que quer atualizar está correto."});
                }
            }

            if(Venda.StatusVenda == EnumStatusVenda.Enviado_para_Transportadora)
            {
                if(status != EnumStatusVenda.Entregue)
                {
                    return BadRequest(new { Erro = "Atualização invalida. Verifique se o status da venda que quer atualizar está correto."});
                }
            }
            
            VendaBanco.StatusVenda = status;
            _VendasContext.Vendas.Update(VendaBanco);
            _VendasContext.SaveChanges();

            return Ok(VendaBanco);
        }

        [HttpGet("Visualizar_Venda_Por_Id{id}")]
        public IActionResult ObterVendaPorId(int id)
        {
            var venda = _VendasContext.Vendas.Find(id);

            if (venda == null)
            {
                return NotFound();
            }

            return Ok(venda);
        }

}


}
