using LojaApi.Data.Dto;
using LojaApi.Models;

namespace LojaApi.Interface
{
    public interface ICaixa
    {
        public List<Caixa> CadastrarCaixas(List<CaixaDto> caixas);
        public List<Caixa> ConsultarCaixas();
    }
}
