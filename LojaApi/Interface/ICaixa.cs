using LojaApi.Models;

namespace LojaApi.Interface
{
    public interface ICaixa
    {
        public Caixa CadastrarCaixa(Caixa caixa);
        public List<Caixa> ConsultarCaixas();
    }
}
