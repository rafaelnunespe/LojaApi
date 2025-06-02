using System.Collections.Generic;
using LojaApi.Data.Dto;
using LojaApi.Interface;
using LojaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedido _pedido;

        public PedidoController(IPedido pedido)
        {
            _pedido = pedido;
        }

        [HttpGet]
        [Route("consultaPedidos")]
        public IActionResult ConsultaPedidos()
        {
            return Ok(_pedido.ConsultarPedidos());
        }

        [HttpPost]
        [Route("cadastroPedidos")]
        public IActionResult CadastroPedido([FromBody] List<PedidoInputDto> pedidos)
        {
            if (_pedido.CadastrarPedido(pedidos))
                return Ok(new { message = "Pedidos salvos com sucesso!" });
            else
                return BadRequest(new { message = "Erro ao cadastrar pedidos!" });
        }

        [HttpPost]
        [Route("empacotarPedidos")]
        public IActionResult EmpacotarPedido([FromBody] List<PedidoInputDto> pedidosDto)
        {
            return Ok(_pedido.ProcessarPedidos(pedidosDto));
        }
    }
}
