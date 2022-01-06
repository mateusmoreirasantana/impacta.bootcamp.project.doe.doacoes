using System;
namespace impacta.bootcamp.project.doe.doacoes.api.Models.Doacoes
{
    public class DoacaoRequest
    {
        public int campanhaId { get; set; }
        public PagamentoCartao pagamento_cartao { get; set; }
        public bool exibe_valorDoacao { get; set; }
        public bool exibe_nomeDoador { get; set; }
        public string comentario { get; set; }
        public double valor { get; set; }
        public string transaction { get; set; }
    }

    public class PagamentoCartao
    {
        public string num_cartao { get; set; }
        public string nome_cartao { get; set; }
        public string dt_validade { get; set; }
        public string CVV { get; set; }
    }


}
