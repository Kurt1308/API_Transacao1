using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoDto.RequisicoesDto
{
    /// <summary>
    /// Requisição para obter transação por id
    /// </summary>
    public class RequisicaoGetByIdTransacaoDto
    {
        /// <summary>
        /// Id da transação
        /// </summary>
        /// <exemplo>10</exemplo>
        public Guid id_transacao { get; set; }
    }
}
