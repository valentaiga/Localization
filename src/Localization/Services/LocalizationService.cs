using Localization.Models;

namespace Localization.Services;

public class LocalizationService : ILocalizationService
{
    private readonly ILocalizationProvider _localizationProvider;

    public LocalizationService(ILocalizationProvider localizationProvider)
    {
        _localizationProvider = localizationProvider;
    }

    public async ValueTask<string> GetString(string path, string culture, CancellationToken ct = default)
    {
        var result = await _localizationProvider.Get(path, culture, ct);
        return result.Result;
    }

    public ValueTask<LocalizationResult> Get(string path, string culture, CancellationToken ct = default)
    {
        return _localizationProvider.Get(path, culture, ct);
    }
}