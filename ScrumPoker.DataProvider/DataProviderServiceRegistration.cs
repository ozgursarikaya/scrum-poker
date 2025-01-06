using Microsoft.Extensions.DependencyInjection;

namespace ScrumPoker.DataProvider;

public static class DataProviderServiceRegistration
{
    public static IServiceCollection AddDataProviderServices(this IServiceCollection services)
    {
        services.AddScoped<IDataProvider, DapperDataProvider>();

        return services;
    }
}