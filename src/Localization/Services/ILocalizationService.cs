using Localization.Models;

namespace Localization.Services;

public interface ILocalizationService
{
    ValueTask<string> GetString(string path, string culture, CancellationToken ct = default);
    ValueTask<LocalizationResult> Get(string path, string culture, CancellationToken ct = default);
}