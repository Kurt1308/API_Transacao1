using Adaptador.Interfaces;
using Aplicacao.Interface;
using AplicacaoDto.RequisicoesDto;
using AplicacaoDto.RespostaDto.transacaoDto;
using Core.Interface.Servico;
using Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class AplicacaoTransacao : IAplicacaoTransacao
    {
        private const string erroInserir = "NÃO FOI POSSÍVEL CADASTRAR TRANSAÇÃO";
        private const string semDados = "NÃO POSSUI DADOS";
        private const string erroAtualizar = "NÃO FOI POSSÍVEL ATUALIZAR ESSA TRANSAÇÃO";
        private readonly IMapperTransacao _mapperTransacao;
        private readonly IServicoTransacao _servicoTransacao;

        public AplicacaoTransacao(IServicoTransacao servicoTransacao, IMapperTransacao mapperTransacao)
        {
            _servicoTransacao = servicoTransacao;
            _mapperTransacao = mapperTransacao;
        }

        public RespostaPutTransacaoDto AtualizarTransacao(RequisicaoPutTransacaoDto obj)
        {
            string mensagem = "";
            if (!mensagem.Equals(string.Empty))
                return _mapperTransacao.MapperToDtoPut(HttpStatusCode.UnprocessableEntity, mensagem);
            try
            {
                transacao retornoTransacao = _servicoTransacao.atualizarTransacao(obj.id_transacao, obj.num_cartao, obj.valor);

                return _mapperTransacao.MapperToDtoPut(retornoTransacao != null ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, retornoTransacao != null ? mensagem : erroAtualizar, retornoTransacao);
            }
            catch (Exception erro)
            {
                return _mapperTransacao.MapperToDtoPut(HttpStatusCode.InternalServerError, erro.Message);
            }
        }

        public RespostaGetTransacaoDto GetAllTransacoes()
        {
            try
            {
                List<transacao> retorno = _servicoTransacao.buscaTransacoes().ToList();
                return _mapperTransacao.MapperToDtoTransacao(retorno.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound, retorno.Count > 0 ? "" : semDados, retorno);
            }
            catch (Exception erro)
            {
                return _mapperTransacao.MapperToDtoTransacao(HttpStatusCode.InternalServerError, erro.Message);
            }
        }

        public RespostaGetByIdTransacaoDto GetTransacaoById(RequisicaoGetByIdTransacaoDto dto)
        {
            string msgErroValidacao = "";
            if (!msgErroValidacao.Equals(string.Empty))
                return _mapperTransacao.MapperToDtoGetTransacao(HttpStatusCode.UnprocessableEntity, msgErroValidacao, null);

            try
            {
                var cadastroTransacao = _servicoTransacao.GetTransacaoById(dto.id_transacao);

                if (cadastroTransacao == null)
                    return _mapperTransacao.MapperToDtoGetTransacao(HttpStatusCode.NotFound, "Consulta não retornou dados, verifique os parâmetros enviados", null);
                else
                    return _mapperTransacao.MapperToDtoGetTransacao(HttpStatusCode.OK, "", cadastroTransacao);
            }
            catch (Exception Erro)
            {
                return _mapperTransacao.MapperToDtoGetTransacao(HttpStatusCode.InternalServerError, Erro.ToString(), null);
            }
        }

        public RespostaInsertTransacaoDto Insert(RequisicaoInsertTransacaoDto obj)
        {
            string mensagem = "";

            if (!mensagem.Equals(string.Empty))
                return _mapperTransacao.MapperToDtoInsert(HttpStatusCode.UnprocessableEntity, mensagem);
            try
            {
                
                transacao retornoTransacao = _servicoTransacao.insereTransacao(obj.num_cartao, obj.valor, obj.id_aprovacao);
                return _mapperTransacao.MapperToDtoInsert(retornoTransacao != null ? HttpStatusCode.Created : HttpStatusCode.InternalServerError, retornoTransacao != null ? mensagem : erroInserir, retornoTransacao);
            }
            catch (Exception erro)
            {
                return _mapperTransacao.MapperToDtoInsert(HttpStatusCode.InternalServerError, erro.Message);
            }
        }

        public static async Task Get()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(requestUri: "https://localhost:5014/GetTodosOsCartoes");

            var data = await response.Content.ReadAsStringAsync();
        }
    }
}
