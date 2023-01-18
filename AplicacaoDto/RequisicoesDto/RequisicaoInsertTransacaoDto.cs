using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoDto.RequisicoesDto
{
    /// <summary>
    /// Requisição para cadastrar transação
    /// </summary>
    public class RequisicaoInsertTransacaoDto
    {
        /// <summary>
        /// Número do cartao 
        /// </summary>
        /// <exemplo>20356</exemplo>
        public long num_cartao { get; set; }
        /// <summary>
        /// Id da aprovação
        /// </summary>
        /// <exemplo>10</exemplo>
        public Guid id_aprovacao { get; set; }
        /// <summary>
        /// Valor da transação
        /// </summary>
        /// <exemplo>100,00</exemplo>
        public decimal valor { get; set; }
    }
}
