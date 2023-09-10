using Elevator.Application.Common;
using Elevator.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionTest
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IRepository, Repository>();

        return services;
    }
}
