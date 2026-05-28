using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Filters;

public sealed class TranslationFilterAttribute : TypeFilterAttribute
{
    public TranslationFilterAttribute(bool noCache = false) : base(typeof(TranslationFilter))
    {
        Arguments = [noCache];
    }
}