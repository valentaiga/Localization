using Localization.Models;

namespace Localization.Services;

public interface ILocalizationProvider
{
    public ValueTask<LocalizationResult> Get(string path, string culture, CancellationToken ct = default);
}