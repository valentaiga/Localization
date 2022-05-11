using System.Xml;
using Localization.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Localization.Extensions;

public static class XmlLocalizationExtensions
{
    public static IServiceCollection AddXmlLocalizator(this IServiceCollection services, Action<XmlReaderSettings> configureXmlSettings = null)
    {
        services.AddSingleton<ILocalizationProvider, XmlLocalizationProvider>();
        services.AddSingleton<XmlReaderSettings>(_ =>
        {
            var settings = new XmlReaderSettings
            {
                Async = true,
                IgnoreWhitespace = true
            };
            if (configureXmlSettings is not null) configureXmlSettings(settings);
            return settings;
        });
        return services;
    }
}