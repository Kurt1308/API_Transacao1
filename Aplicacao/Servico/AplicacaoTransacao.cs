using Adaptador.Interfaces;
using Aplicacao.Interface;
using AplicacaoDto.RequisicoesDto;
using AplicacaoDto.RespostaDto.transacaoDto;
using Core.Interface.Servico;
using Dominio.Entidade;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
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
            bool check = VerificaLimiteCartao(obj.valor, obj.num_cartao);
            if(check == false)
                return _mapperTransacao.MapperToDtoInsert(HttpStatusCode.Forbidden, "Este cartão não possui limite disponível");
            if (!mensagem.Equals(string.Empty))
                return _mapperTransacao.MapperToDtoInsert(HttpStatusCode.UnprocessableEntity, mensagem);
            try
            {
                
                transacao retornoTransacao = _servicoTransacao.insereTransacao(obj.num_cartao, obj.valor, obj.id_aprovacao);
                return _mapperTransacao.MapperToDtoInsert(retornoTransacao != null ? HttpStatusCode.Created : HttpStatusCode.InternalServerError, retornoTransacao != null ? "Transação efetuada com sucesso" : erroInserir, retornoTransacao);
            }
            catch (Exception erro)
            {
                return _mapperTransacao.MapperToDtoInsert(HttpStatusCode.InternalServerError, erro.Message);
            }
        }
        public static bool VerificaLimiteCartao(decimal valor, long num_cartao)
        {

            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5014/GetCartaoPorId");

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var objeto = new { id_cartao = 0, fkAgencia = 0, idConta = 0 };

            var objeto1 = selecionaCartao(objeto, num_cartao);

            var response = client.PostAsync("https://localhost:5014/GetCartaoPorId", objeto1).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                var json = JsonConvert.SerializeObject(dataObjects);
                if (dataObjects.Contains("limite_saldo_disponivel"))
                {
                    
                    var valor22 = Convert.ToDecimal(GetValor(dataObjects));
                    int v1 = Convert.ToInt32(valor);
                    decimal v2 = valor22;

                    if (v1 <= v2)
                    return true;
                }
            }
            else
            {
                return false;
            }

            return false;
        }
        public static string GetValor(string dataObjects)
        {
            if (dataObjects.Contains("limite_saldo_disponivel"))
            {
                var aiai = dataObjects.Split("limite_saldo_disponivel")[1];
                var eiei = aiai.Split(':')[1];
                var yy = eiei.Split(',')[0];
                var ww = yy.Replace('.', ',');
                return ww;
            }
            return dataObjects;
        }
        private static StringContent selecionaCartao(object obj, long num_cartao)
        {
            if(num_cartao == 2000000000000000)
            {
                var objeto = new { id_cartao = 12, fkAgencia = 2, idConta = 5 };
                var json = JsonConvert.SerializeObject(objeto);
                var data = new StringContent(json, Encoding.UTF8, mediaType: "application/json");
                return data;
            } else if ( num_cartao == 1000000000000000)
            {
                var objeto1 = new { id_cartao = 11, fkAgencia = 1, idConta = 6 };
                var json1 = JsonConvert.SerializeObject(objeto1);
                var data1 = new StringContent(json1, Encoding.UTF8, mediaType: "application/json");
                return data1;
            }
            var json2 = JsonConvert.SerializeObject(obj);
            var data2 = new StringContent(json2, Encoding.UTF8, mediaType: "application/json");
            return data2;
        }
        //public static async Task<object> Post()
        //{
        //    var httpClient = new HttpClient();

        //    var request = new HttpRequestMessage();

        //    var objeto = new { id_cartao = 1, fkAgencia = 1, idConta = 6 };

        //    var content = ToRequest(objeto);

        //    var response = await httpClient.PostAsync(requestUri: "https://localhost:5014/GetCartaoPorId", content);

        //    var data = await response.Content.ReadAsStringAsync();
        //    return data;
        //}
        //private static StringContent ToRequest(object obj)
        //{
        //    var json = JsonConvert.SerializeObject(obj);
        //    var data = new StringContent(json, Encoding.UTF8, mediaType: "application/json");

        //    return data;
        //}

        //public static async Task Get()
        //{
        //    var httpClient = new HttpClient();

        //    var response = await httpClient.GetAsync(requestUri: "https://localhost:5014/GetTodosOsCartoes");

        //    var data = await response.Content.ReadAsStringAsync();
        //}
    }
}
