using System.Xml;
using System.Xml.Linq;
using Localization.Configuration;
using Localization.Models;

namespace Localization.Services;

public class XmlLocalizationProvider : ILocalizationProvider
{
    private readonly XmlReaderSettings _xmlSettings;
    private static SemaphoreSlim Slim = new SemaphoreSlim(1, 1);
    
    private readonly string _directory;
    private readonly Dictionary<string, string> _localizations;

    public XmlLocalizationProvider(LocalizationSettings settings, XmlReaderSettings xmlSettings)
    {
        _xmlSettings = xmlSettings;
        _directory = settings.ResourcesDirectory;
        _localizations = settings.Resources.ToDictionary(x => x.Culture, x => x.FullFileName);
    }

    public async ValueTask<LocalizationResult> Get(string path, string culture, CancellationToken ct = default)
    {
        var srcPath = GetResourceFilePath(culture);
        if (srcPath is null)
            return LocalizationResult.Null();

        await Slim.WaitAsync(ct);

        var result = await ReadFromFile(path, srcPath, ct);

        Slim.Release();
        
        return result is null
            ? LocalizationResult.Null()
            : LocalizationResult.Success(result);
    }
    
    private string GetResourceFilePath(string culture)
    {
        if (!_localizations.TryGetValue(culture, out var fullFileName))
            return null;

        return Path.Combine(_directory, fullFileName);
    }

    private async Task<string> ReadFromFile(string xmlPath, string filePath, CancellationToken ct)
    {
        await using var fs = File.OpenRead(filePath);
        using var xmlReader = XmlReader.Create(fs, _xmlSettings);
        await xmlReader.MoveToContentAsync();

        var el = (XElement)await XNode.ReadFromAsync(xmlReader, ct);
        foreach (var val in xmlPath.Split(":"))
        {
            while (el.Name != val)
            {
                if (el.NextNode is null)
                    return null;
                el = (XElement)el.NextNode;
            }

            if (el.FirstNode is XElement xElement)
                el = xElement;
        }

        return el.Value;
    } 

    private static async Task<string> ReadFromFile(string filePath, CancellationToken ct)
    {
        var text = await File.ReadAllTextAsync(filePath, ct);
        return text;
        // await using var fs = File.OpenRead(filePath);
    } 
}