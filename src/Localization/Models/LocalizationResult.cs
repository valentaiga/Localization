namespace Localization.Models;

public class LocalizationResult
{
    public string Result { get; }
    public bool ResourceFound { get; }

    private LocalizationResult()
    {
    }

    private LocalizationResult(string result)
    {
        Result = result;
        ResourceFound = true;
    }

    public static LocalizationResult Null()
        => new();

    public static LocalizationResult Success(string result)
        => new(result);
}