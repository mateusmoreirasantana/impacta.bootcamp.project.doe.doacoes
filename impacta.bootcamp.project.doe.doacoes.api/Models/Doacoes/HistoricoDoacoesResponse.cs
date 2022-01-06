using System;
namespace impacta.bootcamp.project.doe.doacoes.api.Models.Doacoes
{
    public class HistoricoDoacoesResponse:BaseResponse
    {
        public bool exibirValorDoado { get; set; }
        public double valorDoado { get; set; }
        public bool exibirNomeDoador { get; set; }
        public string nomeDoador { get; set; }
        public DateTime dataDoacao { get; set; }
        public Campanhas campanhas { get; set; }
    }

    public class SolicitacaoVoluntario
    {
        public bool? status { get; set; }
        public string descricaoVaga { get; set; }
    }

    public class Campanhas
    {
        public string img_background { get; set; }
        public string img_background_card { get; set; }
        public int id_campanha { get; set; }
        public string nome_campanha { get; set; }
        public string desc_campanha { get; set; }
        public double valorTotal { get; set; }
        public string tipoCampanha { get; set; }
        public object dataLimite { get; set; }
        public bool status { get; set; }
        public double valorAtual { get; set; }
        public object organizacao { get; set; }
        public SolicitacaoVoluntario solicitacaoVoluntario { get; set; }
        public string tipo_doacao { get; set; }
        public string unidadeMedida { get; set; }
    }


}
