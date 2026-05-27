using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Configuration;

[ExcludeFromCodeCoverage(Justification = "Configuration")]
public sealed class BlobStorageOptions
{
    public const string Name = "BlobStorage";

    public string AccessKey { get; set; } = string.Empty;
    
    public string Endpoint { get; set; } = string.Empty;
}