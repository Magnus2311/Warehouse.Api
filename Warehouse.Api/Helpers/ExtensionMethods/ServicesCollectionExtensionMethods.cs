using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Api.Services;
using Warehouse.Api.Services.Connections;
using Warehouse.Api.Services.Mappers;
using Warehouse.Database.Helpers;
using Warehouse.Database.Repositories;

namespace Warehouse.Api.Helpers.ExtensionMethods
{
    public static class ServicesCollectionExtensionMethods
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services
                .AddScoped<ItemsRepository>()
                .AddScoped<PartnersRepository>()
                .AddScoped<SalesRepository>();

            services
                .AddScoped<ItemsService>()
                .AddScoped<PartnersService>()
                .AddScoped<SalesService>();

            services
                .AddHttpClient<SsoConnectionService>();

            services
                .AddScoped<AppSettings>();

            services.AddMapper();
        }


        private static void AddMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}