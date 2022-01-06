using System;
using System.Threading.Tasks;

namespace impacta.bootcamp.project.doe.doacoes.infra.data.ExternalServices.DoeAuth.Interfaces
{
    public interface IAuthServices
    {

        public Task<core.Entities.DoeAuth.UserAuth> validate(string Token);
    }
}
