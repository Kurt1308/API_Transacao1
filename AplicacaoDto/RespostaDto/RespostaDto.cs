using System.Net;
using System.Text.Json.Serialization;

namespace AplicacaoDto.RespostaDto
{
    /// <summary>
    /// Campos base para DTOs de resposta
    /// </summary>
    public class RespostaDto
    {
        [JsonIgnore]
        /// <summary>
        ///  Código de status de resposta HTTP
        /// </summary>
        public HttpStatusCode codRetorno { get; set; }
        /// <summary>
        ///  Mensagem de retorno
        /// </summary>
        public string Mensagem { get; set; }
    }
}
