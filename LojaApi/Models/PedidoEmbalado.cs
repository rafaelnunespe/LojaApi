using System.ComponentModel.DataAnnotations;

namespace LojaApi.Models
{
    public class PedidoEmbalado
    {
        [Key]
        public int Pedido_id { get; set; }
        public List<CaixaEmbalada> Caixas { get; set; }
    }

    public class CaixaEmbalada
    {
        public string Caixa_id { get; set; }
        public List<string> Produtos { get; set; }
        public string Observacao { get; set; }
    }
}

