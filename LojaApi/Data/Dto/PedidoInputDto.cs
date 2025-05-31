namespace LojaApi.Data.Dto
{
    public class PedidoInputDto
    {
        public int Pedido_Id { get; set; }
        public List<ProdutoInputDto> Produtos { get; set; }
        
    }
}
