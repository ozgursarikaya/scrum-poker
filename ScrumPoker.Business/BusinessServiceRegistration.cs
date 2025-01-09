using Microsoft.Extensions.DependencyInjection;
using ScrumPoker.Business.Abstract;
using ScrumPoker.Business.Concrete;

namespace ScrumPoker.Business;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IPlanningPokerVotingTypeService, PlanningPokerVotingTypeService>();
        services.AddScoped<IPlanningPokerService, PlanningPokerService>();
        services.AddScoped<IRetroService, RetroService>();
        services.AddScoped<IRetroColumnService, RetroColumnService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserVerificationService, UserVerificationService>();
        services.AddScoped<IPlanningPokerUserService, PlanningPokerUserService>();

        return services;
    }
}