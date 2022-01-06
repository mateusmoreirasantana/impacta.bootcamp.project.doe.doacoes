using System;
namespace impacta.bootcamp.project.doe.doacoes.core.DTOs
{
    public class CreateDonationDTO
    {
        public int campanhaId { get; set; }
        public string userName { get; set; }
        public bool exibeValorDoacao { get; set; }
        public bool exibeNomeDoador { get; set; }
        public string comentario { get; set; }
        public double valor { get; set; }
        public string transactionId { get; set; }
        public int formaPagamentId { get; set; }
    }
}
