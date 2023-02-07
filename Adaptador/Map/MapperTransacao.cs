using Adaptador.Interfaces;
using AplicacaoDto.RespostaDto.transacaoDto;
using Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Adaptador.Map
{
    public class MapperTransacao : IMapperTransacao
    {
        public RespostaGetByIdTransacaoDto MapperToDtoGetTransacao(HttpStatusCode codRetorno, string mensagem, transacao item)
        {
            return new RespostaGetByIdTransacaoDto
            {
                codRetorno = codRetorno,
                Mensagem = mensagem,
                transacao = item
            };
        }

        public RespostaInsertTransacaoDto MapperToDtoInsert(HttpStatusCode codRetorno, string mensagem, transacao transacao = null)
        {
            return new RespostaInsertTransacaoDto()
            {
                codRetorno = codRetorno,
                Mensagem = mensagem,
                transacao = transacao
            };
        }

        public RespostaPutTransacaoDto MapperToDtoPut(HttpStatusCode codRetorno, string mensagem, transacao transacao = null)
        {
            return new RespostaPutTransacaoDto()
            {
                codRetorno = codRetorno,
                Mensagem = mensagem,
                transacao = transacao
            };
        }

        public RespostaGetTransacaoDto MapperToDtoTransacao(HttpStatusCode codRetorno, string mensagem, List<transacao> transacao = null)
        {
            return new RespostaGetTransacaoDto()
            {
                codRetorno = codRetorno,
                Mensagem = mensagem,
                transacoes = transacao
            };
        }
    }
}
