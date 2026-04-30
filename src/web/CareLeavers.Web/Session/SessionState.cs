namespace CareLeavers.Web.Session;

public enum SessionState
{
    TranslationUsage,
    PdfUsage
}

public static class SessionUsageLimits
{
    public const int TranslationLimit = 5;
    public const int PdfLimit = 3;
}