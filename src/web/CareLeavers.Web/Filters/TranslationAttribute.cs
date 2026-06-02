using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class TranslationAttribute : TypeFilterAttribute
{
    public TranslationAttribute(bool noCache = false) : base(typeof(TranslationFilter))
    {
        Arguments = [noCache];
    }
}