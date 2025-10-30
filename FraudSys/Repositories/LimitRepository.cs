using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using FraudSys.Models;

namespace FraudSys.Repositories
{
    /// <summary>
    /// Responsável por acessar o banco de dados DynamoDB
    /// para realizar operações de CRUD sobre os limites de conta.
    /// </summary>
    public class LimitRepository
    {
        private readonly DynamoDBContext _context;

        // O repositório utiliza o contexto do DynamoDB para mapear objetos C# ↔ DynamoDB
        public LimitRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        /// <summary>
        /// Cria um novo registro de limite no DynamoDB.
        /// </summary>
        public async Task CreateAsync(AccountLimit limit)
        {
            if (string.IsNullOrEmpty(limit.Cpf) || string.IsNullOrEmpty(limit.Agencia) || string.IsNullOrEmpty(limit.Conta))
            {
                throw new ArgumentException("Cpf, Agencia e Conta são obrigatórios para gerar o Id.");
            }
            //Gera o Id com base nas propriedades antes de salvar
            limit.Id = $"{limit.Cpf}-{limit.Agencia}-{limit.Conta}";
            await _context.SaveAsync(limit);
        }

        /// <summary>
        /// Busca um registro pelo CPF, agência e conta.
        /// </summary>
        public async Task<AccountLimit?> GetAsync(string cpf, string agency, string account)
        {
            var key = $"{cpf}-{agency}-{account}";
            return await _context.LoadAsync<AccountLimit>(key);
        }

        /// <summary>
        /// Atualiza um registro existente.
        /// </summary>
        public async Task UpdateAsync(AccountLimit limit)
        {
            await _context.SaveAsync(limit);
        }

        /// <summary>
        /// Remove um registro existente.
        /// </summary>
        public async Task DeleteAsync(AccountLimit limit)
        {
            await _context.DeleteAsync(limit);
        }
    }
}