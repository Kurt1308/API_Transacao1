using Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoDto.RespostaDto.transacaoDto
{
    /// <summary>
    /// Resposta com uma transação filtrada por id
    /// </summary>
    public class RespostaGetByIdTransacaoDto : RespostaDto
    {
        /// <summary>
        /// Resposta que retorna transação por id
        /// </summary>
        public transacao transacao { get; set; }
    }
}
