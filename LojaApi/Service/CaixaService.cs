using LojaApi.Data;
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
        public Caixa CadastrarCaixa(Caixa caixa)
        {
            _dbContext.Add(caixa);
            _dbContext.SaveChanges();

            return caixa;
        }

        public List<Caixa> ConsultarCaixas()
        {
            return _dbContext.Caixas.ToList();
        }
    }
}
