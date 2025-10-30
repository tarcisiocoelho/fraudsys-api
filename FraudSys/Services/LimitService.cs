using FraudSys.Models;
using FraudSys.Repositories;

//SERVICE REGRAS DE NEGÓCIO

namespace FraudSys.Services
{
    /// <summary>
    /// Camada de serviço responsável pela lógica de negócio relacionada ao gerenciamento de limites PIX
    /// </summary>
    public class LimitService
    {
        /// <summary>
        /// Simulação de armazenamento em memória (posteriormente será substituido pelo DynamoDB
        /// </summary>
        private readonly LimitRepository _repository;

        public LimitService(LimitRepository limitRepository)
        {
            _repository = limitRepository;
        }

        public async Task<AccountLimit> CreateLimitAsync(AccountLimit limit)
        {
            await _repository.CreateAsync(limit);
            return limit;
        }

        public async Task<AccountLimit?> GetLimitAsync(string cpf, string agencia, string conta)
        {
            return await _repository.GetAsync(cpf, agencia, conta);
        }

        public async Task<AccountLimit?> UpdateLimitAsync(AccountLimit updated)
        {
            var existing = await _repository.GetAsync(updated.Cpf, updated.Agencia, updated.Conta); //Buscando pra ver se existe com as informações solicitadas
            if (existing == null)
            {
                return null; //Se o retorno for igual a null, retorna nada
            }
            existing.Limite = updated.Limite; //Se existir, atualiza os dados com os atuais
            await _repository.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteLimitAsync(string cpf, string agencia, string conta)
        {
            var limite = await _repository.GetAsync(cpf, agencia, conta);
            if (limite == null)
            {
                return false;
            }
            await _repository.DeleteAsync(limite);
            return true;
        }
    }
}