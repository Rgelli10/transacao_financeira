using Moq;
using NUnit.Framework;
using transacao_financeira.repositorio;
using transacao_financeira.servico;

namespace transacao_financeira.teste
{
    [TestFixture]
    public class TransacaoFinanceiraTest
    {
        private TransacaoFinanceiraServico _transacaoFinanceiraServico;
        private Mock<IAcessoDados> _acessoDadosMock;

        [SetUp]
        public void SetUp()
        {
            _acessoDadosMock = new Mock<IAcessoDados>();
            _transacaoFinanceiraServico = new TransacaoFinanceiraServico(_acessoDadosMock.Object);
        }

        [Test]
        public void Transferir_SaldoSuficiente_TransferenciaEfetuadaComSucesso()
        {
            // Arrange
            long contaOrigem = 123;
            long contaDestino = 456;
            decimal saldoOrigem = 1000;
            decimal valorTransferencia = 500;

            _acessoDadosMock.Setup(ad => ad.ObterSaldo(contaOrigem))
                .Returns(new modelo.ContaSaldo { Saldo = saldoOrigem });

            _acessoDadosMock.Setup(ad => ad.ObterSaldo(contaDestino))
                .Returns(new modelo.ContaSaldo { Saldo = 100 });

            // Act
            _transacaoFinanceiraServico.Transferir(1, contaOrigem, contaDestino, valorTransferencia);

            // Assert
            _acessoDadosMock.Verify(ad => ad.Atualizar(It.IsAny<modelo.ContaSaldo>()), Times.Exactly(2));
        }

        [Test]
        public void Transferir_ContaOrigemInvalida_TransacaoCancelada()
        {
            // Arrange
            long contaOrigem = 789;
            long contaDestino = 456;

            _acessoDadosMock.Setup(ad => ad.ObterSaldo(contaOrigem))
                .Returns((modelo.ContaSaldo)null);

            // Act
            _transacaoFinanceiraServico.Transferir(2, contaOrigem, contaDestino, 100);

            // Assert
            _acessoDadosMock.Verify(ad => ad.Atualizar(It.IsAny<modelo.ContaSaldo>()), Times.Never);
        }

        [Test]
        public void Transferir_SaldoInsuficiente_TransacaoCancelada()
        {
            // Arrange
            long contaOrigem = 123;
            long contaDestino = 456;
            decimal saldoOrigem = 100;
            decimal valorTransferencia = 500;

            _acessoDadosMock.Setup(ad => ad.ObterSaldo(contaOrigem))
                .Returns(new modelo.ContaSaldo { Saldo = saldoOrigem });

            _acessoDadosMock.Setup(ad => ad.ObterSaldo(contaDestino))
                .Returns(new modelo.ContaSaldo { Saldo = 100 });

            // Act
            _transacaoFinanceiraServico.Transferir(3, contaOrigem, contaDestino, valorTransferencia);

            // Assert
            _acessoDadosMock.Verify(ad => ad.Atualizar(It.IsAny<modelo.ContaSaldo>()), Times.Never);
        }
     }
}