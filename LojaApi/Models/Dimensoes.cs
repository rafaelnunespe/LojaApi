namespace LojaApi.Models
{
    public class Dimensoes
    {
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }
        public int Volume => Altura * Largura * Comprimento;

        public List<(int, int, int)> GetRotations()
        {
            var dims = new[] { Altura, Largura, Comprimento };
            return new List<(int, int, int)>
        {
            (dims[0], dims[1], dims[2]),
            (dims[0], dims[2], dims[1]),
            (dims[1], dims[0], dims[2]),
            (dims[1], dims[2], dims[0]),
            (dims[2], dims[0], dims[1]),
            (dims[2], dims[1], dims[0])
        };
        }
    }
}
