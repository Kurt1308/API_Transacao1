using Adaptador.Interfaces;
using Aplicacao.Interface;
using AplicacaoDto.RequisicoesDto;
using AplicacaoDto.RespostaDto.transacaoDto;
using Core.Interface.Servico;
using Dominio.Entidade;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        
        public RespostaInsertTransacaoDto Insert(RequisicaoInsertTransacaoDto obj, string accessToken)
        {
            string mensagem = "";

            decimal checaLimite = VerificaLimiteCartao(obj.valor, obj.num_cartao);
            if(checaLimite < 0)
                return _mapperTransacao.MapperToDtoInsert(HttpStatusCode.Forbidden, "Este cartão não possui limite disponível.");
            
            bool checkUpdate = UpdateCartao(checaLimite, obj.num_cartao);
            if (!checkUpdate)
                return _mapperTransacao.MapperToDtoInsert(HttpStatusCode.Forbidden, "Não foi possível atualizar o cartão.");

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
        public static bool UpdateCartao(decimal valor, long num_cartao)
        {
                var cartao = selecionarCartao(valor, num_cartao);
                using var client = new HttpClient();
                var response = client.PutAsync("https://localhost:5014/AtualizaCartao", cartao).Result;
            if (response.IsSuccessStatusCode)
            {
                    return true;
            }
            else
            {
                return false;
            }
        }
        private static StringContent selecionarCartao(decimal valor, long num_cartao)
        {
            if (num_cartao == 2000000000000000)
            {
                var objeto = new { id_cartao = 12, agencia_id_agencia = 2, conta_id_conta = 5, limite_saldo = valor, situacao = 1 };
                var json = JsonConvert.SerializeObject(objeto);
                var data = new StringContent(json, Encoding.UTF8, mediaType: "application/json");
                return data;
            }
            else if (num_cartao == 1000000000000000)
            {
                var objeto1 = new { id_cartao = 11, agencia_id_agencia = 1, conta_id_conta = 6, limite_saldo = valor, situacao = 1 };
                var json1 = JsonConvert.SerializeObject(objeto1);
                var data1 = new StringContent(json1, Encoding.UTF8, mediaType: "application/json");
                return data1;
            }
            var objeto2 = new { id_cartao = 0, agencia_id_agencia = 0, conta_id_conta = 0, limite_saldo = 0, situacao = 0 };
            var json2 = JsonConvert.SerializeObject(objeto2);
            var data2 = new StringContent(json2, Encoding.UTF8, mediaType: "application/json");
            return data2;
        }
        public static decimal VerificaLimiteCartao(decimal valor, long num_cartao)
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
                if (dataObjects.Contains("limite_saldo"))
                {
                    
                    var valor22 = Convert.ToDecimal(GetValor(dataObjects));
                    decimal v1 = valor;
                    decimal v2 = valor22;

                    decimal v = v2 - v1;
                    return v;
                }
            }
            else
            {
                decimal a = -1;
                return a;
            }
            decimal b = -1;
            return b;
        }
        public static string GetValor(string dataObjects)
        {
            if (dataObjects.Contains("limite_saldo"))
            {
                var processo1 = dataObjects.Split("limite_saldo")[1];
                var processo2 = processo1.Split(':')[1];
                var processo3 = processo2.Split(',')[0];
                var valor = processo3.Replace('.', ',');
                return valor;
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
    }
}
