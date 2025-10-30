using FraudSys.Models;
using FraudSys.Repositories;

namespace FraudSys.Services
{
    /// <summary>
    /// Serviço responsável por processar transações PIX com base no limite disponível do cliente
    /// </summary>
    public class PixService
    {
        private readonly LimitRepository _repository;

        public PixService(LimitRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Avalia e processa a transação PIX
        /// </summary>
        /// <param name="transacao"></param>
        /// <returns></returns>
        public async Task<(bool Aprovado, string Mensagem, AccountLimit? Conta)> ProcessarPixAsync(PixTransaction transacao)
        {
            //Busca os dados da conta no DynamoDB;
            var conta = await _repository.GetAsync(transacao.Cpf, transacao.Agencia, transacao.Conta);
            if (conta == null)
            {
                return (false, "Conta não encontrada", null);
            }

            //Verifica se há limite suficiente
            if (conta.Limite < transacao.ValorTransferencia)
            {
                return (false, "Transação negada: limite insuficiente.", conta);
            }

            //Aprova e desconta o valor do limite
            conta.Limite -= transacao.ValorTransferencia;

            //Atualiza o registro no banco
            await _repository.UpdateAsync(conta);

            //Retorna resultado de sucesso
            return (true, "Transação aprovada.", conta);
        }
    }
}