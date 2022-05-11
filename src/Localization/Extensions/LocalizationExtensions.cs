using Localization.Configuration;
using Localization.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Localization.Extensions;

public static class LocalizationExtensions
{
    public static IServiceCollection AddLocalizator(this IServiceCollection services, Action<LocalizationSettings> configureSettings = null)
    {
        services.AddSingleton<LocalizationSettings>(_ =>
        {
            var settings = new LocalizationSettings();
            if (configureSettings is not null) configureSettings(settings);
            return settings;
        });
        services.AddSingleton<ILocalizationService, LocalizationService>();

        return services;
    }
}