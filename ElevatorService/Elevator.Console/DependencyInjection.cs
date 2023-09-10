using Elevator.Application.Commands.AddElevator;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConsoleServices(this IServiceCollection services)
        {
            //services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(AddElevatorCommand))));

            return services;
        }
    }
}
