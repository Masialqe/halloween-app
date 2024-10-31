using HalloweenApp.Application.Abstractions;
using HalloweenApp.Infrastrukture.Data.Memory;

namespace HalloweenApp.Infrastrukture.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IEventRepository, EventRepository>();
            return services;
        }
    }
}
