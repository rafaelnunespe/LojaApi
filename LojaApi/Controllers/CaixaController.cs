using LojaApi.Interface;
using LojaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaixaController : ControllerBase
    {
        private readonly ICaixa _caixa;

        public CaixaController(ICaixa caixa)
        {
            _caixa = caixa;
        }

        [HttpPost]
        [Route("cadastroCaixa")]
        public IActionResult CadastroCaixa([FromBody] Caixa caixa)
        {
            return Ok(_caixa.CadastrarCaixa(caixa));
        }

        [HttpGet]
        [Route("consultaCaixa")]
        public IActionResult ConsultaCaixa()
        {
            return Ok(_caixa.ConsultarCaixas());
        }
    }
}
