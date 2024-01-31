using transacao_financeira.repositorio;

namespace transacao_financeira.servico
{
    public class TransacaoFinanceiraServico : ITransacaoFinanceiraServico
    {
        private readonly IAcessoDados _acessoDados;

        public TransacaoFinanceiraServico(IAcessoDados acessoDados)
        {
            _acessoDados = acessoDados;
        }

        public void Transferir(int correlationId, long contaOrigem, long contaDestino, decimal valor)
        {
            var contaSaldoOrigem = _acessoDados.ObterSaldo(contaOrigem);

            if(contaSaldoOrigem is null)
            {
                Console.WriteLine($"Transacao numero {correlationId} foi cancelada porque a conta {contaOrigem} é invalida");
                return;
            }
            
            if(contaSaldoOrigem.Saldo < valor)
            {
                Console.WriteLine($"Transacao numero {correlationId} foi cancelada por falta de saldo");
                return;
            }
            
            var contaSaldoDestino = _acessoDados.ObterSaldo(contaDestino);

            if(contaSaldoDestino is null)
            {
                Console.WriteLine($"Transacao numero {correlationId} foi cancelada porque a conta {contaDestino} é invalida");
                return;
            }

            contaSaldoOrigem.Saldo -= valor;
            contaSaldoDestino.Saldo += valor;
            _acessoDados.Atualizar(contaSaldoOrigem);
            _acessoDados.Atualizar(contaSaldoDestino);
            Console.WriteLine($"Transacao numero {correlationId} foi efetivada com sucesso! Novos saldos: Conta Origem:{contaSaldoOrigem.Saldo} | Conta Destino: {contaSaldoDestino.Saldo}");
        }
    }
}