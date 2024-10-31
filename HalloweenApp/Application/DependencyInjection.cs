using HalloweenApp.Application.Commands;
using HalloweenApp.Components.Events;

namespace HalloweenApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<AddEventHandler>();
            services.AddScoped<LoadEventsHandler>();

            return services;
        }
    }
}
