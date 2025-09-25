using System.Diagnostics.CodeAnalysis;
using CareLeavers.Web.Models.Content;

namespace CareLeavers.Web.Models;

[ExcludeFromCodeCoverage(Justification = "Standard ASP.NET Core error model")]
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    
    public Page? Page { get; set; }
}