using Core.Interface.Repositorio;
using Data;
using Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.RepositorioTransacao
{
    public class RepositorioTransacao : RepositoryBase<transacao>, IRepositorioTransacao
    {
        private readonly SqlContext _context;

        public RepositorioTransacao(SqlContext Context) : base(Context)
        {
            _context = Context;
        }

        public IQueryable<transacao> buscaTransacoes()
        {
            return _context.transacao.AsQueryable();
        }

        public transacao GetTransacaoById(Guid id)
        {
            return _context.transacao.Where(x => x.id_transacao == id).FirstOrDefault();
        }

        public transacao insereTransacao(long num_cartao, decimal valor, Guid id_aprovacao)
        {
            var result = _context.transacao.SingleOrDefault(x => x.num_cartao == num_cartao);
            if (result == null)
            {
                transacao transacao = new transacao()
                {
                    num_cartao = num_cartao,
                    valor = valor,
                    id_aprovacao = id_aprovacao
                };
                var data = DateTime.UtcNow;
                transacao.data_transacao = data;
                _context.transacao.Add(transacao);
                _context.SaveChanges();

                return transacao;
            }

            return null;
        }
    }
}
