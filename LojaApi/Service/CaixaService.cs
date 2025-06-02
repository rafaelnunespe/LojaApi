using LojaApi.Data;
using LojaApi.Data.Dto;
using LojaApi.Interface;
using LojaApi.Models;

namespace LojaApi.Service
{
    public class CaixaService : ICaixa
    {
        private readonly DataBaseContext _dbContext;

        public CaixaService(DataBaseContext dataBaseContext)
        {
            _dbContext = dataBaseContext;
        }
        public List<Caixa> CadastrarCaixas(List<CaixaDto> caixas)
        {
            List<Caixa> pacotes = caixas.Select(dto => new Caixa
            {
                Nome = dto.Nome,
                Dimensoes = new Dimensoes
                {
                    Altura = dto.Dimensoes.Altura,
                    Largura = dto.Dimensoes.Largura,
                    Comprimento = dto.Dimensoes.Comprimento
                }
            }).ToList();
            
            try
            {
                _dbContext.Caixas.AddRange(pacotes);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao empacotar pedidos!");
            }

            return pacotes;
        }

        public List<Caixa> ConsultarCaixas()
        {
            return _dbContext.Caixas.ToList();
        }
    }
}
