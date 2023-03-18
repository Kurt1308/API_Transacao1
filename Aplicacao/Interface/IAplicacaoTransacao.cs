using AplicacaoDto.RequisicoesDto;
using AplicacaoDto.RespostaDto.transacaoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interface
{
    public interface IAplicacaoTransacao
    {
        RespostaInsertTransacaoDto Insert(RequisicaoInsertTransacaoDto requisicaoInsertTransacaoDto);
        RespostaGetTransacaoDto GetAllTransacoes();
        RespostaGetByIdTransacaoDto GetTransacaoById(RequisicaoGetByIdTransacaoDto dto);
        RespostaPutTransacaoDto AtualizarTransacao(RequisicaoPutTransacaoDto dto, string accessToken);
    }
}
