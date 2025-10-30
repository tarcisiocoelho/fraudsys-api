namespace FraudSys.Models
{
    /// <summary>
    /// Representa uma transação PIX enviada pelo cliente.
    /// </summary>
    public class PixTransaction
    {
        public string Cpf { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public decimal ValorTransferencia { get; set; } //Valor que o cliente quer transferir via PIX
    }
}