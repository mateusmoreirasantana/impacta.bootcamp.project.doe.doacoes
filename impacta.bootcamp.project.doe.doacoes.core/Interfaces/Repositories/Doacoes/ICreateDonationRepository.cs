using System;
using System.Threading.Tasks;
using impacta.bootcamp.project.doe.doacoes.core.DTOs;

namespace impacta.bootcamp.project.doe.doacoes.core.Interfaces.Repositories.Doacoes
{
    public interface ICreateDonationRepository
    {
        public Task<OperationCreateDonationDTO> create(CreateDonationDTO request);
    }
}

