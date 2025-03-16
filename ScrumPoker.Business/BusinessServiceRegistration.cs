using Microsoft.Extensions.DependencyInjection;
using ScrumPoker.Business.Abstract;
using ScrumPoker.Business.Concrete;
using ScrumPoker.Logger.Concrete;

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
        services.AddScoped<ILoggerService, NLogService>();
        services.AddScoped<IUserTokenService, UserTokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}