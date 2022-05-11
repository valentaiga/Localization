namespace Localization.Configuration;

public class LocalizationResource
{
    public string FullFileName { get; }
    public string Culture { get; }

    public LocalizationResource(string culture, string fullFileName)
    {
        Culture = culture;
        FullFileName = fullFileName;
    }
}