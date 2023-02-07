using Dominio.Entidade;

namespace AplicacaoDto.RespostaDto.transacaoDto
{
    /// <summary>
    /// Classe de resposta relacionada a transação 
    /// </summary>
    public class RespostaInsertTransacaoDto : RespostaDto
    {
        /// <summary>
        /// Resposta ao inserir, devolve um objeto do tipo transacao
        /// </summary>
        public transacao transacao { get; set; }
    }
}
