using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BDTest.Data;
using BDTest.Domain;
using BDTest.IAPI.Client;
using BDTest.Client;

namespace BDTest.DependencyInjection
{
    public class BDRoot
    {
        public static void InjectIAPI(IServiceCollection services)
        {
            InjectDomainDependencies(services);
        }

        public static void InjectEAPI(IServiceCollection services)
        {
            InjectDataDependencies(services);

            InjectDomainDependencies(services);   

            InjectEAPIDependencies(services);
        }

        private static void InjectDataDependencies(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opts => 
            {
                opts
                    .UseInMemoryDatabase("BDTest", DatabaseRoot.InMemoryDatabaseRoot)
                    .EnableSensitiveDataLogging();

            }, ServiceLifetime.Transient);
            
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }

        private static void InjectDomainDependencies(IServiceCollection services)
        {
            services.AddScoped<IGenerator, From1to100Generator>();
            services.AddScoped<IMultiplier, RandomMultiplier>();
        }

        private static void InjectEAPIDependencies(IServiceCollection services)
        {
            services.AddSingleton<IGenerateServiceClient, GenerateServiceClient>();
            services.AddSingleton<IMultiplyServiceClient, MultiplyServiceClient>();
            
            services.AddSingleton<IGrpgClientFactory, GrpgClientFactory>(s => 
            {
                return new GrpgClientFactory("https://localhost:6011");
            });
        }

    }
}
