using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoDto.RequisicoesDto
{
    /// <summary>
    /// Requisicão para atualizar transação
    /// </summary>
    public class RequisicaoPutTransacaoDto
    {
        /// <summary>
        /// Id do cartao
        /// </summary>
        /// <exemplo>10</exemplo>
        public Guid id_transacao { get; set; }
        /// <summary>
        /// Número do cartao 
        /// </summary>
        /// <exemplo>20356</exemplo>
        public long num_cartao { get; set; }
        /// <summary>
        /// Valor da transação
        /// </summary>
        /// <exemplo>100,00</exemplo>
        public decimal valor { get; set; }
    }
}
