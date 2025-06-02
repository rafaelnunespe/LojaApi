using System.ComponentModel.DataAnnotations;
using LojaApi.Interface;

namespace LojaApi.Models
{
    public class Caixa
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public Dimensoes Dimensoes { get; set; }
        
    }
}
