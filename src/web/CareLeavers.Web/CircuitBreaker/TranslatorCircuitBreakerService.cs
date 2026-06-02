using System.Text;
using System.Text.Json;
using Azure.Storage.Blobs;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Translation;
using Microsoft.Extensions.Options;

namespace CareLeavers.Web.CircuitBreaker;

public sealed class TranslatorCircuitBreakerData
{
    public int CharacterCount { get; set; }
    public DateTime TimeStamp { get; set; }
}

public sealed class TranslatorCircuitBreakerService(
    BlobServiceClient blobServiceClient,
    IOptions<BlobStorageOptions> blobStorageOptions,
    IOptions<AzureTranslationOptions> azureTranslationOptions) : ITranslatorCircuitBreakerService
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly string _containerName = blobStorageOptions.Value.ContainerName;
    private readonly string _blobName = blobStorageOptions.Value.BlobName;
    private readonly int _characterLimit = azureTranslationOptions.Value.CharacterLimit;

    public async Task<bool> ShouldOpenCircuit(string html)
    {
        BlobClient blobClient = blobServiceClient.GetBlobContainerClient(_containerName).GetBlobClient(_blobName);
        DateTime dateNow = DateTime.UtcNow;

        TranslatorCircuitBreakerData circuitBreakerData = await DownloadBlob(blobClient) ??
                                                          new TranslatorCircuitBreakerData
                                                              { CharacterCount = 0, TimeStamp = dateNow };

        if (ShouldResetCircuit(dateNow, circuitBreakerData.TimeStamp))
        {
            circuitBreakerData.CharacterCount = 0;
            circuitBreakerData.TimeStamp = dateNow;
        }

        if (circuitBreakerData.CharacterCount >= _characterLimit) return true;

        circuitBreakerData.CharacterCount += html.Length;

        await UploadBlob(circuitBreakerData, blobClient);

        return false;
    }

    private async Task<TranslatorCircuitBreakerData?> DownloadBlob(BlobClient blobClient)
    {
        if (!await blobClient.ExistsAsync()) return null;

        using MemoryStream memoryStream = new();
        await blobClient.DownloadToAsync(memoryStream);

        if (memoryStream.Length == 0) return null;

        memoryStream.Seek(0, SeekOrigin.Begin);
        return await JsonSerializer.DeserializeAsync<TranslatorCircuitBreakerData>(memoryStream, JsonSerializerOptions);
    }

    private static async Task UploadBlob(TranslatorCircuitBreakerData data, BlobClient blobClient)
    {
        using MemoryStream memoryStream =
            new(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data, JsonSerializerOptions)));
        await blobClient.UploadAsync(memoryStream, overwrite: true);
    }

    private static bool ShouldResetCircuit(DateTime dateNow, DateTime blobDate) =>
        dateNow.Month != blobDate.Month || dateNow.Year != blobDate.Year;
}