using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Pdf;

public interface IPdfGenerator
{
    Task<byte[]> Generate(string url, string collectionId, string languageCode, List<string> tags);
}

public sealed class PdfGenerator(HttpClient httpClient, IFusionCache fusionCache) : IPdfGenerator
{
    public async Task<byte[]> Generate(string url, string collectionId, string languageCode, List<string> tags)
    {
        return await fusionCache.GetOrSetAsync<byte[]>($"pdf:{collectionId}:{languageCode}",
            async cancellationToken =>
            {
                using MultipartFormDataContent formData = new();

                formData.Add(new StringContent(url), "url");

                using HttpResponseMessage response =
                    await httpClient.PostAsync("/forms/chromium/convert/url", formData, cancellationToken);
                
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsByteArrayAsync(cancellationToken);
            }, tags: tags);
    }
}