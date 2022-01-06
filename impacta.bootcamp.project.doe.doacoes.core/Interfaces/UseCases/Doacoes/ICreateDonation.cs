using System;
using System.Threading.Tasks;
using impacta.bootcamp.project.doe.doacoes.core.DTOs;

namespace impacta.bootcamp.project.doe.doacoes.core.Interfaces.UseCases.Doacoes
{
    public interface ICreateDonation
    {
        public Task<OperationCreateDonationDTO> create(CreateDonationDTO request);
    }
}
