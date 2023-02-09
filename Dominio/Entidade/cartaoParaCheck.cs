using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade
{
    public class cartaoParaCheck
    {
        public class cartao
        {
            /// <summary>
            /// Id do cartao
            /// </summary>
            /// <exemplo>10</exemplo>
            [Key]
            public int? id_cartao { get; set; }
            /// <summary>
            /// Número do cartao 
            /// </summary>
            /// <exemplo>20356</exemplo>
            public long num_cartao { get; set; }
            /// <summary>
            /// Mês e ano de vencimento do cartão no formato AAAAMM
            /// </summary>
            /// <exemplo>202005</exemplo>
            public string mes_ano_vencimento { get; set; }
            /// <summary>
            /// Código de segurança do cartão
            /// </summary>
            /// <exemplo>365</exemplo>
            public int cvc { get; set; }
            /// <summary>
            /// Id da agencia relacionada a conta
            /// </summary>
            /// <exemplo>2</exemplo>
            public int? agencia_id_agencia { get; set; }
            /// <summary>
            /// Id da conta relacionada ao contão
            /// </summary>
            /// <exemplo>2</exemplo>
            public int? conta_id_conta { get; set; }
            /// <summary>
            /// Limite de saldo do cartão
            /// </summary>
            /// <exemplo>1000,00</exemplo>
            public decimal? limite_saldo { get; set; }
            /// <summary>
            /// Limite de saldo disponível do cartão
            /// </summary>
            /// <exemplo>100,00</exemplo>
            public decimal? limite_saldo_disponivel { get; set; }
            /// <summary>
            /// Situação atual da conta
            /// </summary>
            /// <exemplo>Ativa; Inativa</exemplo>
            public int? situacao { get; set; }
        }
    }
}
