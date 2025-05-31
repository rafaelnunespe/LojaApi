using LojaApi.Models;

namespace LojaApi.Data.Dto
{
    public class ProdutoOutputDto
    {
        public string ProdutoId { get; set; }

        public Dimensoes Dimensoes { get; set; }

        public int PedidoId { get; set; }
    }
}
