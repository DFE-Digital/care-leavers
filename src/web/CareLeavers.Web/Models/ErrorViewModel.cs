using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Models;

[ExcludeFromCodeCoverage(Justification = "Standard ASP.NET Core error model")]
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}