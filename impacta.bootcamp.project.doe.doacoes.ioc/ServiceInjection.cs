using System;
using impacta.bootcamp.project.doe.doacoes.infra.data.Data.Context;
using impacta.bootcamp.project.doe.doacoes.infra.data.ExternalServices.DoeAuth.Interfaces;
using impacta.bootcamp.project.doe.doacoes.infra.data.ExternalServices.DoeAuth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace impacta.bootcamp.project.doe.doacoes.ioc
{
    public class ServiceInjection
    {

        private static ServiceProvider _provider;

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<SqlContext>();
            services.AddScoped<impacta.bootcamp.project.doe.doacoes.core.Interfaces.UseCases.Doacoes.ICreateDonation, impacta.bootcamp.project.doe.doacoes.application.UseCases.Donation.CreateDonationUseCase>();
            services.AddScoped<impacta.bootcamp.project.doe.doacoes.core.Interfaces.Repositories.Doacoes.ICreateDonationRepository, impacta.bootcamp.project.doe.doacoes.infra.data.Data.Repositories.CreateDonationRepository>();

            services.AddScoped<IAuthServices, AuthServices>();
        

            _provider = services.BuildServiceProvider();
        }

        public static ServiceProvider GetServiceProvider()
        {
            return _provider;
        }
    }
}
