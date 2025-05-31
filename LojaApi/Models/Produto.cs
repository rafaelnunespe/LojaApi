using System.ComponentModel.DataAnnotations;

namespace LojaApi.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string ProdutoId { get; set; }

        public Dimensoes Dimensoes { get; set; }


        // Chave estrangeira para Pedido
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        
    }
}
