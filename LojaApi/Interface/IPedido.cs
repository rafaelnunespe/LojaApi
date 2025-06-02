using LojaApi.Data.Dto;
using LojaApi.Models;

namespace LojaApi.Interface
{
    public interface IPedido
    {
        public bool CadastrarPedido(List<PedidoInputDto> pedidos);
        public List<ProdutoOutputDto> ConsultarPedidos();
        public List<PedidoEmbalado> ProcessarPedidos(List<PedidoInputDto> pedidosDto);
    }
}
