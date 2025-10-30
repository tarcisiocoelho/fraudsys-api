using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace FraudSys.Models
{
    [DynamoDBTable("AccountLimits")] //Mapeando para a tabela "AccountLimits" no DynamoDB
    public class AccountLimit
    {
        /// <summary>
        /// Representa os dados de limite de transação PIX de uma conta
        /// </summary>
        /// 
        [DynamoDBHashKey]
        public string Id { get; set; }
        [DynamoDBProperty]
        public string Cpf { get; set; }
        [DynamoDBProperty]
        public string Agencia { get; set; }
        [DynamoDBProperty]
        public string Conta { get; set; }
        [DynamoDBProperty]
        public decimal Limite { get; set; }
    }
}