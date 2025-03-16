using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace ScrumPoker.Localization
{
	public static class LocalizationServiceExtensions
	{
		public static IServiceCollection AddLocalizationServices(this IServiceCollection services)
		{
			services.AddLocalization(options => options.ResourcesPath = "Resources");

			services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
			services.AddSingleton<IStringLocalizer<SharedResource>, StringLocalizer<SharedResource>>();

			return services;
		}
	}
}
