using FraudSys.Models;
using FraudSys.Services;
using Microsoft.AspNetCore.Mvc;

namespace FraudSys.Controllers
{
    /// <summary>
    /// Controller responsável por receber as requisições http e direcionar para a chamada de serviço (Limite Service)
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LimitsController : ControllerBase
    {
        //Injeção de dependência. Serviço responsável pelas regras de negócio.
        private readonly LimitService _limitService;

        //Construtor - o .NET faz a injeção de dependência automaticamente
        public LimitsController(LimitService limitService)
        {
            _limitService = limitService;
        }

        /// <summary>
        /// Cria um novo limite para o cliente
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateLimit([FromBody] AccountLimit limit)
        {
            var created = await _limitService.CreateLimitAsync(limit);
            return Ok(created);
        }

        /// <summary>
        /// Busca o limite cadastrado para um cliente específico
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// 
        [HttpGet("{cpf}/{agencia}/{conta}")]
        public async Task<IActionResult> GetLimit(string cpf, string agencia, string conta)
        {
            var limit = await _limitService.GetLimitAsync(cpf, agencia, conta);
            if (limit == null)
            {
                return NotFound("Conta não foi encontrada");
            }
            return Ok(limit);
        }

        /// <summary>
        /// Atualiza o limite de uma conta
        /// </summary>
        /// <param name="updated"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateLimit([FromBody] AccountLimit updated)
        {
            var limit = await _limitService.UpdateLimitAsync(updated);
            if (limit == null)
            {
                return NotFound("Conta não foi encontrada");
            }
            return Ok(limit);
        }

        /// <summary>
        /// Deletando um limite.
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="agencia"></param>
        /// <param name="conta"></param>
        /// <returns></returns>
        [HttpDelete("{cpf}/{agencia}/{conta}")]
        public async Task<IActionResult> DeleteLimit(string cpf, string agencia, string conta)
        {
            var success = await _limitService.DeleteLimitAsync(cpf, agencia, conta);
            if (!success)
            {
                return NotFound("Conta não encontrada");
            }
            return Ok("Registro removido com sucesso.");
        }
    }
}