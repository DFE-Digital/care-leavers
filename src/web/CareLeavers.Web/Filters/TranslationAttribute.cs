using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Filters;

public sealed class TranslationAttribute : TypeFilterAttribute
{
    public TranslationAttribute(bool noCache = false) : base(typeof(TranslationFilter))
    {
        Arguments = [noCache];
    }
}