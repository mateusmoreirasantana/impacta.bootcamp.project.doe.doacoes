using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using impacta.bootcamp.project.doe.doacoes.api.Models.Doacoes;
using impacta.bootcamp.project.doe.doacoes.api.Models.Errors;
using impacta.bootcamp.project.doe.doacoes.core.DTOs;
using impacta.bootcamp.project.doe.doacoes.core.Interfaces.UseCases.Doacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace impacta.bootcamp.project.doe.doacoes.api.Endpoints.Doacoes
{
    public class Create : BaseEndPoint<DoacaoRequest, DoacaoResponse>
    {
        private readonly ICreateDonation useCase;
        public Create(ICreateDonation createUseCase)

        {
            useCase = createUseCase;
        }
        /// <summary>
        /// registra uma nova doacao
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("doacoes/create")]
        public async override Task<ActionResult<DoacaoResponse>> HandleAsync(DoacaoRequest request, CancellationToken cancellationToken = default)
        {
            DoacaoResponse response = new DoacaoResponse();



            try
            {
                if (!ModelState.IsValid)
                {
                    IEnumerable<ModelError> modelErrors = ModelState.Values.SelectMany(v => v.Errors);
                    string msg = $"Erro ao criar doacao, request invalido" + modelErrors.ElementAt(0).ErrorMessage;

                    var responseError = new DoacaoResponse()
                    {
                        status = "400",
                        error = new Models.Errors.Error() { errorCode = "MODVAL01", errorMessage = msg }

                    };
                    return BadRequest(responseError);

                }
                var model = ModelToDTO(request);

                // model.user = "damian_lindgren@yahoo.com";
                model.userName = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Email)?.Value;
                var create = await useCase.create(model);
                var createResponse = dtoToResponse(create);



                if (createResponse.status == "400")
                {
                    return BadRequest(createResponse);
                }
                return Ok(createResponse);
            }
            catch (Exception ex)
            {
                string msg = $"Erro ao criar campanha" + ex.ToString();

                var resp = new DoacaoResponse()
                {
                    status = "500",
                    error = new Models.Errors.Error() { errorCode = "MODVAL01", errorMessage = msg }

                };
                return StatusCode(StatusCodes.Status500InternalServerError, resp);

            }

        }
        private DoacaoResponse dtoToResponse(OperationCreateDonationDTO operationCreateDTO)
        {
            var response = new DoacaoResponse();

            if (!operationCreateDTO.sucesso)

            {

                if (operationCreateDTO.error != null)
                {
                    Error error = new Error() { errorCode = operationCreateDTO.error.errorCode, errorMessage = operationCreateDTO.error.errorMessage };

                    response.error = error;
                }
                response.status = "400";

            }
            else
            {
                response.status = "200";
            }

            return response;
        }

        private CreateDonationDTO ModelToDTO(DoacaoRequest request)
        {

            CreateDonationDTO dto = new CreateDonationDTO()
            {
              campanhaId = request.campanhaId,
              exibeValorDoacao = request.exibe_valorDoacao,
              exibeNomeDoador = request.exibe_nomeDoador,
              comentario = request.comentario,
              valor = request.valor,
               transactionId = request.transaction,
               formaPagamentId = (request.pagamento_cartao != null ? 2:1)
            };
          

            return dto;

        }
    }
}
