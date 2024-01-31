using transacao_financeira.modelo;

namespace transacao_financeira.repositorio
{
    public class AcessoDados : IAcessoDados
    {
        private readonly List<ContaSaldo> _contasSaldo;

        public AcessoDados()
        {
            _contasSaldo = new List<ContaSaldo>
            {
                new ContaSaldo { Conta = 938485762, Saldo = 180 },
                new ContaSaldo { Conta = 347586970, Saldo = 1200 },
                new ContaSaldo { Conta = 2147483649, Saldo = 0 },
                new ContaSaldo { Conta = 675869708, Saldo = 4900 },
                new ContaSaldo { Conta = 238596054, Saldo = 478 },
                new ContaSaldo { Conta = 573659065, Saldo = 787 },
                new ContaSaldo { Conta = 210385733, Saldo = 10 },
                new ContaSaldo { Conta = 674038564, Saldo = 400 },
                new ContaSaldo { Conta = 563856300, Saldo = 1200 }
            };
        }

        public ContaSaldo ObterSaldo(long id)
        {
            return _contasSaldo.Find(x => x.Conta == id)!;
        }

        public bool Atualizar(ContaSaldo contaSaldo)
        {
            try
            {
                _contasSaldo.RemoveAll(x => x.Conta == contaSaldo.Conta);
                _contasSaldo.Add(contaSaldo);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}