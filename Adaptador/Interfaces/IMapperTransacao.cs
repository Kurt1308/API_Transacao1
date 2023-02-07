using AplicacaoDto.RespostaDto.transacaoDto;
using Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Adaptador.Interfaces
{
    public interface IMapperTransacao
    {
        RespostaPutTransacaoDto MapperToDtoPut(HttpStatusCode codRetorno, string mensagem, transacao transacao = null);
        RespostaInsertTransacaoDto MapperToDtoInsert(HttpStatusCode codRetorno, string mensagem, transacao transacao = null);
        RespostaGetTransacaoDto MapperToDtoTransacao(HttpStatusCode codRetorno, string mensagem, List<transacao> transacao = null);
        RespostaGetByIdTransacaoDto MapperToDtoGetTransacao(HttpStatusCode codRetorno, string mensagem, transacao item);
    }
}
