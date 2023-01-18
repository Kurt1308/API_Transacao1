using Dominio.Entidade;
using System.Collections.Generic;

namespace AplicacaoDto.RespostaDto.transacaoDto
{
    /// <summary>
    /// Respostas contendo lista de transações registradas
    /// </summary>
    public class RespostaGetTransacaoDto : RespostaDto
    {
        /// <summary>
        /// Resposta que retorna uma lista com todas as transações registradas
        /// </summary>
        public List<transacao> transacoes { get; set; }
    }
}
