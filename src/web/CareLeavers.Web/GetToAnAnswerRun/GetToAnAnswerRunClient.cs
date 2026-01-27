using System.Text;
using HtmlAgilityPack;
using Joonasw.AspNetCore.SecurityHeaders.Csp;
using Microsoft.Extensions.Primitives;

namespace CareLeavers.Web.GetToAnAnswerRun;

public class GetToAnAnswerRunClient(HttpClient httpClient, IServiceProvider serviceProvider) : IGetToAnAnswerRunClient
{
    private readonly IConfiguration _configuration = serviceProvider.GetRequiredService<IConfiguration>();
    private readonly ICspNonceService _cspNonceService = serviceProvider.GetRequiredService<ICspNonceService>();

    public async Task<string> GetStartPageOrInitialState(string languageCode, string questionnaireSlug)
    {
        var responseMessage = await httpClient.GetAsync(
            $"/questionnaires/{questionnaireSlug}/start?embed=true");
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get start state for questionnaire {questionnaireSlug}");
        }
        
        var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
        var html = Encoding.UTF8.GetString(bytes);
        
        // Replace the base url with the local url so that the embedded content redirects to the correct page
        return SubstitutePageContent(languageCode, html);
    }

    public async Task<string> GetInitialState(string languageCode, string questionnaireSlug)
    {
        var responseMessage = await httpClient.GetAsync(
            $"/questionnaires/{questionnaireSlug}/next?embed=true");
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get initial state for questionnaire {questionnaireSlug}");
        }
        
        var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
        var html = Encoding.UTF8.GetString(bytes);
        
        // Replace the base url with the local url so that the embedded content redirects to the correct page
        return SubstitutePageContent(languageCode, html);
    }

    public async Task<string> GetNextState(string thisOrigin, string languageCode, string questionnaireSlug, Dictionary<string, StringValues> formData)
    {
        // Flatten the dictionary for FormUrlEncodedContent
        var formContent = formData
            .SelectMany(kvp => kvp.Value, (kvp, value) => new KeyValuePair<string, string>(kvp.Key, value ?? string.Empty));

        var responseMessage = await httpClient.PostAsync(
            $"/questionnaires/{questionnaireSlug}/next?embed=true", 
            new FormUrlEncodedContent(formContent));
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get next state for questionnaire {questionnaireSlug}");
        }
        
        var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
        var html = Encoding.UTF8.GetString(bytes);
        
        // Replace the base url with the local url so that the embedded content redirects to the correct page
        return SubstitutePageContent(languageCode, html, thisOrigin);
    }

    public async Task<(Stream fileStream, string contentType)> GetDecorativeImage(string questionnaireSlug)
    {
        var response = await httpClient.GetAsync($"/questionnaires/{questionnaireSlug}/decorative-image");
    
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get decorative image for questionnaire {questionnaireSlug}");
        }
    
        var stream = await response.Content.ReadAsStreamAsync();
        var contentType = response.Content.Headers.ContentType?.MediaType ?? "image/png";
    
        return (stream, contentType);
    }

    private string SubstitutePageContent(string languageCode, string html, string? thisOrigin = null)
    {
        var doc = new HtmlDocument();
        
        doc.OptionOutputAsXml = false;
        doc.OptionWriteEmptyNodes = true;
        doc.OptionDefaultStreamEncoding = Encoding.UTF8;
        
        doc.LoadHtml(html);
        
        // Inject nonce into script and style tags
        InjectBaseUrlAndNonce(languageCode, doc, thisOrigin);
        
        using var writer = new StringWriter();
        doc.Save(writer);
        return writer.ToString();
    }
    
    private void InjectBaseUrlAndNonce(string languageCode, HtmlDocument doc, string? thisOrigin = null)
    {
        var baseUrl = _configuration["GetToAnAnswer:BaseUrl"];
        var nonce = _cspNonceService.GetNonce();
        
        // Add nonce to all script tags that don't already have one
        var scriptTags = doc.DocumentNode.SelectNodes("//script");
        if (scriptTags != null)
        {
            foreach (var script in scriptTags)
            {
                if (!script.Attributes.Contains("nonce") || string.IsNullOrWhiteSpace(script.Attributes["nonce"].Value))
                {
                    script.SetAttributeValue("nonce", nonce);
                }
                
                if (script.Attributes.Contains("src") && script.Attributes["src"].Value.StartsWith("/"))
                {
                    script.SetAttributeValue("src", baseUrl + script.Attributes["src"].Value);
                }
                
                script.Attributes.Remove("asp-add-nonce");
            }
        }

        // Add baseUrls to all link tags that don't already have one
        var linkTags = doc.DocumentNode.SelectNodes("//link");
        if (linkTags != null)
        {
            foreach (var link in linkTags)
            {
                if (link.Attributes.Contains("href") && link.Attributes["href"].Value.StartsWith("/"))
                {
                    link.SetAttributeValue("href", baseUrl + link.Attributes["href"].Value);
                }
                
                link.Attributes.Remove("asp-add-nonce");
            }
        }

        // Add nonce to all style tags that don't already have one
        var styleTags = doc.DocumentNode.SelectNodes("//style");
        if (styleTags != null)
        {
            foreach (var style in styleTags)
            {
                if (!style.Attributes.Contains("nonce") || string.IsNullOrWhiteSpace(style.Attributes["nonce"].Value))
                {
                    style.SetAttributeValue("nonce", nonce);
                }
                
                style.Attributes.Remove("asp-add-nonce");
            }
        }
        
        // Add nonce to all style tags that don't already have one
        var formTags = doc.DocumentNode.SelectNodes("//form");
        if (formTags != null)
        {
            foreach (var form in formTags)
            {
                if (form.Attributes.Contains("action") && form.Attributes["action"].Value.StartsWith("/questionnaires/"))
                {
                    form.SetAttributeValue("action", form.Attributes["action"].Value
                        .Replace("/questionnaires", $"/{languageCode}/get-to-an-answer-questionnaires"));
                }
            }
        }
        
        // Add nonce to all style tags that don't already have one
        var anchorTags = doc.DocumentNode.SelectNodes("//a");
        if (anchorTags != null)
        {
            foreach (var anchor in anchorTags)
            {
                if (anchor.Attributes.Contains("href") && anchor.Attributes["href"].Value.StartsWith("/questionnaires/"))
                {
                    anchor.SetAttributeValue("href", anchor.Attributes["href"].Value
                        .Replace("/questionnaires", $"/{languageCode}/get-to-an-answer-questionnaires"));
                }
            }
        }
        
        // Add nonce to all style tags that don't already have one
        var imgTags = doc.DocumentNode.SelectNodes("//img");
        if (imgTags != null)
        {
            foreach (var img in imgTags)
            {
                if (img.Attributes.Contains("src") && img.Attributes["src"].Value.Contains("/decorative-image"))
                {
                    img.SetAttributeValue("src", img.Attributes["src"].Value
                        .Replace("/questionnaires", $"/{languageCode}/get-to-an-answer-questionnaires"));
                }
            }
        }
        
        // if the external link is this site, change the language code 
        var externalLinkInput = doc.DocumentNode.SelectSingleNode("//input[@id='external-link-dest']");
        if (externalLinkInput != null && thisOrigin != null)
        {
            // if 'externalLinkInput.value' starts with 'thisOrigin' (https://*.support-for-care-leavers.education.gov.uk)
            // then replace the language code in the url with the current translation language code
            
            var url = new Uri(externalLinkInput.Attributes["value"].Value);
            
            if (url.Host.Equals(thisOrigin))
            {
                var pathParts = url.AbsolutePath.Split('/');

                if (pathParts.Length > 1)
                {
                    pathParts[1] = languageCode;
                }
                
                var newUrl = new UriBuilder(url) {Path = string.Join('/', pathParts)}.Uri;
                
                externalLinkInput.SetAttributeValue("value", newUrl.ToString());
            }
        }
    }
}