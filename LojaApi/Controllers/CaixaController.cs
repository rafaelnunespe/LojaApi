using LojaApi.Data.Dto;
using LojaApi.Interface;
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
        [Route("cadastroCaixas")]
        public IActionResult CadastroCaixa([FromBody] List<CaixaDto> caixas)
        {
            return Ok(_caixa.CadastrarCaixas(caixas));
        }

        [HttpGet]
        [Route("consultaCaixas")]
        public IActionResult ConsultaCaixa()
        {
            return Ok(_caixa.ConsultarCaixas());
        }
    }
}
