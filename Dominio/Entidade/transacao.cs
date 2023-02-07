using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade
{
    /// <summary>
    /// Entidade que mapeia tabela transacao no banco de dados
    /// </summary>
    public class transacao
    {
        /// <summary>
        /// Id da transação
        /// </summary>
        /// <exemplo>10</exemplo>
        [Key]
        public Guid id_transacao { get; set; }
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
        /// Data da transação
        /// </summary>
        /// <exemplo>2023-12-15 15:55:02</exemplo>
        public DateTime data_transacao { get; set; }
        /// <summary>
        /// Valor da transação
        /// </summary>
        /// <exemplo>100,00</exemplo>
        public decimal? valor { get; set; }
    }
}
