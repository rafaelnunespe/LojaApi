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
        //public int VolumeRestante { get; set; }
        //public List<Produto> Produtos { get; set; } = new List<Produto>();

        //public bool TentaAdicionar(Produto produto)
        //{
        //    if (!ProdutoCabe(produto)) return false;

        //    if (produto.Dimensoes.Volume <= VolumeRestante)
        //    {
        //        VolumeRestante -= produto.Dimensoes.Volume;
        //        return true;
        //    }

        //    return false;
        //}

        //private bool ProdutoCabe(Produto produto)
        //{
        //    foreach (var (a, l, c) in produto.Dimensoes.GetRotations())
        //    {
        //        if (a <= Dimensoes.Altura && l <= Dimensoes.Largura && c <= Dimensoes.Comprimento)
        //            return true;
        //    }
        //    return false;
        //}
    }
}
