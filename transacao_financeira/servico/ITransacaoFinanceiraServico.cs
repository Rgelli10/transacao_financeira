namespace transacao_financeira.servico
{
    public interface ITransacaoFinanceiraServico
    {
        void Transferir(int correlationId, long contaOrigem, long contaDestino, decimal valor);
    }
}