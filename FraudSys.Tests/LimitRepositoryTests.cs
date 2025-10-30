using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FraudSys.Tests.Repositories
{
    // Modelo simples
    public class Limit
    {
        public string Id { get; set; }
        public string Cpf { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
    }

    // Fake repository inline
    public class FakeLimitRepository
    {
        private readonly List<Limit> _limits = new();

        public Task SaveAsync(Limit limit)
        {
            if (limit == null) throw new ArgumentNullException(nameof(limit));
            if (string.IsNullOrEmpty(limit.Cpf)) throw new ArgumentException("CPF obrigatório");

            // Se o Id não foi setado, gera um automaticamente
            if (string.IsNullOrEmpty(limit.Id))
            {
                limit.Id = $"{limit.Cpf}-{limit.Agencia}-{limit.Conta}";
            }

            _limits.Add(limit);
            return Task.CompletedTask;
        }

        public Task<Limit> GetAsync(string id)
        {
            return Task.FromResult(_limits.FirstOrDefault(l => l.Id == id));
        }

    }

    public class LimitRepositoryTests
    {
        [Fact]
        public async Task DeveLancarExcecao_SeCamposObrigatoriosForemNulos()
        {
            // Arrange
            var repository = new FakeLimitRepository();
            var limit = new Limit { Cpf = null }; // CPF é obrigatório

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => repository.SaveAsync(limit));
        }

        [Fact]
        public async Task DeveSalvar_LimitValido()
        {
            var repository = new FakeLimitRepository();
            var limit = new Limit { Cpf = "12345678900", Agencia = "001", Conta = "1234" };

            await repository.SaveAsync(limit);

            // Agora usamos o Id que foi gerado automaticamente
            var saved = await repository.GetAsync(limit.Id);

            Assert.NotNull(saved); // agora sim, deve encontrar
            Assert.Equal("12345678900", saved.Cpf);
            Assert.Equal("001", saved.Agencia);
            Assert.Equal("1234", saved.Conta);
        }

    }
}
