using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Configuration;

[ExcludeFromCodeCoverage(Justification = "Configuration")]
public sealed class BlobStorageOptions
{
    public const string Name = "BlobStorage";
    
    public string ContainerName { get; set; } = string.Empty;
    public string BlobName { get; set; } = string.Empty;
}