using Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repositorio
{
    public interface IRepositorioTransacao
    {
        transacao insereTransacao(long num_cartao, decimal valor, Guid id_aprovacao);
        IQueryable<transacao> buscaTransacoes();
        transacao GetTransacaoById(Guid id);
        transacao atualizarTransacao(Guid id_transacao, long num_cartao, Decimal valor);
    }
}
