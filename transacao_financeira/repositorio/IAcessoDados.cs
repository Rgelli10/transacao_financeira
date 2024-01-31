using transacao_financeira.modelo;

namespace transacao_financeira.repositorio
{
    public interface IAcessoDados
    {
        ContaSaldo ObterSaldo(long id);
        bool Atualizar(ContaSaldo dado);
    }
}