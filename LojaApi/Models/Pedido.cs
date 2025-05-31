﻿using System.ComponentModel.DataAnnotations;

namespace LojaApi.Models
{
    public class Pedido
    {

        [Key]
        public int PedidoId { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
