using System;
using System.Threading.Tasks;
using impacta.bootcamp.project.doe.doacoes.core.DTOs;
using impacta.bootcamp.project.doe.doacoes.core.Entities.Settings;
using impacta.bootcamp.project.doe.doacoes.core.Interfaces.Repositories.Doacoes;
using impacta.bootcamp.project.doe.doacoes.core.Interfaces.UseCases.Doacoes;

namespace impacta.bootcamp.project.doe.doacoes.application.UseCases.Donation
{
    public class CreateDonationUseCase:ICreateDonation
    {
        private readonly ICreateDonationRepository repo;


        public CreateDonationUseCase(ICreateDonationRepository createRepository)
        {
            repo = createRepository;
           
        }


        public async Task<OperationCreateDonationDTO> create(CreateDonationDTO request)
        {
            OperationCreateDonationDTO operationCreateDTO = new OperationCreateDonationDTO()
            {
                sucesso = true
            };
            operationCreateDTO = await isValidCreate(request);
            if (operationCreateDTO.sucesso)
            {
             
                operationCreateDTO = await repo.create(request);
            }

            return operationCreateDTO;
        }


        private async Task<OperationCreateDonationDTO> isValidCreate(CreateDonationDTO request)
        {
            OperationCreateDonationDTO operationCreateDTO = new OperationCreateDonationDTO()
            {
                sucesso = true
            };
            if (request.campanhaId <= 0)
            {
                operationCreateDTO.error = new ErrorDTO()
                {
                    errorCode = "VAL01",
                    errorMessage = "campanhaId e obrigatorio"
                };
                operationCreateDTO.sucesso = false;
                return operationCreateDTO;

            }
            if (request.valor <= 0)
            {
                operationCreateDTO.error = new ErrorDTO()
                {
                    errorCode = "VAL02",
                    errorMessage = "valor  e obrigatorio"
                };
                operationCreateDTO.sucesso = false;
                return operationCreateDTO;

            }

            return operationCreateDTO;
        }
        }
}
