using FraudSys.Models;
using FraudSys.Services;
using Microsoft.AspNetCore.Mvc;

namespace FraudSys.Controllers
{
    /// <summary>
    /// Controller responsável por receber e processar transações PIX dos clientes.
    /// </summary>
    /// 
    [ApiController]
    [Route("api/[controller]")]
    public class PixController : ControllerBase
    {
        private readonly PixService _pixService;

        public PixController(PixService pixService)
        {
            _pixService = pixService;
        }

        /// <summary>
        /// Processa uma transação PIX verificando o limite disponível
        /// </summary>
        /// <param name="transacao"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ProcessarPix([FromBody] PixTransaction transacao)
        {
            var (aprovado, mensagem, conta) = await _pixService.ProcessarPixAsync(transacao);

            if (!aprovado)
            {
                return BadRequest(new { status = "Negado", mensagem, limiteAtual = conta?.Limite });
            }
            return Ok(new { status = "Aprovado", mensagem, limiteRestante = conta?.Limite });
        }

    }
}