using Microsoft.Extensions.DependencyInjection;
using ScrumPoker.DataAccess.Abstract;
using ScrumPoker.DataAccess.Concrete;

namespace ScrumPoker.DataAccess;

public static class DataAccessServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddScoped<IPlanningPokerVotingTypeDal, PlanningPokerVotingTypeDal>();
        services.AddScoped<IPlanningPokerDal, PlanningPokerDal>();
        services.AddScoped<IRetroDal, RetroDal>();
        services.AddScoped<IRetroColumnDal, RetroColumnDal>();

        return services;
    }
}