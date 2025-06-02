using System.ComponentModel.DataAnnotations;

namespace LojaApi.Data.Dto
{
    public class PedidoEmbaladoDto
    {
        public List<CaixaEmbaladaDto> Caixas { get; set; }
    }

    public class CaixaEmbaladaDto
    {
        public string Caixa_id { get; set; }
        public List<string> Produtos { get; set; }
        public string Observacao { get; set; }
    }
}
