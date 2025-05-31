using LojaApi.Models;

namespace LojaApi.Data.Dto
{
    public class ProdutoInputDto
    {
        public string Produto_id { get; set; }
        public DimensoesDto Dimensoes { get; set; }
    }
}
