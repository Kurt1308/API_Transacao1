using Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoDto.RespostaDto.transacaoDto
{
    /// <summary>
    /// Resposta para atuazalição de transação
    /// </summary>
    public class RespostaPutTransacaoDto : RespostaDto
    {
        /// <summary>
        /// Resposta ao atualizar, devolve um objeto do tipo transação
        /// </summary>
        public transacao transacao { get; set; }
    }
}
