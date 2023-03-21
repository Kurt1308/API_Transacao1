using Aplicacao.Interface;
using AplicacaoDto.RequisicoesDto;
using AplicacaoDto.RespostaDto.transacaoDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Transacao.Controllers
{
    public class CRUDTransacaoController : Controller
    {
        private readonly IAplicacaoTransacao _aplicacaoTransacao;

        public CRUDTransacaoController(IAplicacaoTransacao aplicacaoTransacao)
        {
            _aplicacaoTransacao = aplicacaoTransacao;
        }
        /// <summary>
        /// Endpoint para atualizar transação 
        /// </summary>
        /// <param name="dto"></param>
        /// <response code="200">Busca realizada com sucesso</response>
        /// <response code="400">Campos obrigatórios não informados</response>
        /// <response code="401">Acesso não autenticado</response>
        /// <response code="403">Acesso não autorizado</response>
        /// <response code="422">Erro de válidação de conteúdo, vide campo mensagem</response>
        /// <response code="500">Erro interno na aplicação, vide campo mensagem</response>
        [ProducesResponseType(typeof(RespostaPutTransacaoDto), 200)]
        [ProducesResponseType(typeof(RespostaPutTransacaoDto), 422)]
        [ProducesResponseType(typeof(RespostaPutTransacaoDto), 500)]
        [HttpPut("AtualizaCartao")]
        public RespostaPutTransacaoDto Put([FromBody] RequisicaoPutTransacaoDto dto)
        {
            var retorno = _aplicacaoTransacao.AtualizarTransacao(dto);
            HttpContext.Response.StatusCode = (int)retorno.codRetorno;
            return retorno;
        }
        /// <summary>
        /// Endpoint para consultar uma determinada transação
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Retorna XXX</returns>
        /// <response code="200">Esta requisição foi bem sucedida</response>
        /// <response code="400">O servidor não entendeu a requisição pois está com uma sintaxe inválida</response>
        /// <response code="422">Erro de validação de conteúdo, código não localizado</response>
        /// <response code="500">Erro interno na aplicação, vide campo mensagem</response>
        [ProducesResponseType(typeof(RespostaGetByIdTransacaoDto), 200)]
        [ProducesResponseType(typeof(RespostaGetByIdTransacaoDto), 400)]
        [ProducesResponseType(typeof(RespostaGetByIdTransacaoDto), 422)]
        [ProducesResponseType(typeof(RespostaGetByIdTransacaoDto), 500)]
        [HttpPost]
        [Route("GetTransacaoPorId")]
        public RespostaGetByIdTransacaoDto GetTransacaoById([FromBody] RequisicaoGetByIdTransacaoDto dto)
        {
            var retorno = _aplicacaoTransacao.GetTransacaoById(dto);
            HttpContext.Response.StatusCode = (int)retorno.codRetorno;
            return retorno;
        }
        /// <summary>
        /// Endpoint que retorna todas as transações registradas
        /// </summary>
        /// <response code="200">Busca realizada com sucesso</response>
        /// <response code="400">Campos obrigatórios não informados</response>
        /// <response code="401">Acesso não autenticado</response>
        /// <response code="403">Acesso não autorizado</response>
        /// <response code="404">Consulta não retornou dados</response>
        /// <response code="500">Erro interno na aplicação, vide campo mensagem</response>
        [ProducesResponseType(typeof(RespostaGetTransacaoDto), 200)]
        [ProducesResponseType(typeof(RespostaGetTransacaoDto), 404)]
        [ProducesResponseType(typeof(RespostaGetTransacaoDto), 500)]
        [AllowAnonymous]
        [HttpGet]
        [Route("GetTodasAsTransacoes")]
        public RespostaGetTransacaoDto GetAllTransaceos()
        {
            return _aplicacaoTransacao.GetAllTransacoes();
        }
        /// <summary>
        /// Endpoint para inserir transação 
        /// </summary>
        /// <param name="dto"></param>
        /// <response code="201">Cadastro realizado com sucesso</response>
        /// <response code="400">Campos obrigatórios não informados</response>
        /// <response code="401">Acesso não autenticado</response>
        /// <response code="403">Acesso não autorizado</response>
        /// <response code="422">Erro de válidação de conteúdo, vide campo mensagem</response>
        /// <response code="500">Erro interno na aplicação, vide campo mensagem</response>
        [ProducesResponseType(typeof(RespostaInsertTransacaoDto), 201)]
        [ProducesResponseType(typeof(RespostaInsertTransacaoDto), 422)]
        [ProducesResponseType(typeof(RespostaInsertTransacaoDto), 500)]
        [HttpPost]
        [Route("InserirTransacao")]
        public RespostaInsertTransacaoDto Insert([FromBody] RequisicaoInsertTransacaoDto dto)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"];
            var retorno = _aplicacaoTransacao.Insert(dto, accessToken);
            HttpContext.Response.StatusCode = (int)retorno.codRetorno;
            return retorno;
        }
    }
}
