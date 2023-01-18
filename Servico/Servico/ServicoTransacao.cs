using Core.Interface.Repositorio;
using Core.Interface.Servico;
using Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servico.Servico
{
    public class ServicoTransacao : IServicoTransacao
    {
        private readonly IRepositorioTransacao _repositorioTransacao;
        public ServicoTransacao(IRepositorioTransacao repositorioTransacao)
        {
            _repositorioTransacao = repositorioTransacao;
        }

        public IQueryable<transacao> buscaTransacoes()
        {
            return _repositorioTransacao.buscaTransacoes();
        }

        public transacao GetTransacaoById(Guid id)
        {
            return _repositorioTransacao.GetTransacaoById(id);
        }

        public transacao insereTransacao(long num_cartao, decimal valor, Guid id_aprovacao)
        {
            return _repositorioTransacao.insereTransacao(num_cartao, valor, id_aprovacao);
        }
    }
}
