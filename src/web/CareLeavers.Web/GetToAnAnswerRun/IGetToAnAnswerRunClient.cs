using Microsoft.Extensions.Primitives;

namespace CareLeavers.Web.GetToAnAnswerRun;

public interface IGetToAnAnswerRunClient
{
    Task<string> GetStartPageOrInitialState(string languageCode, string questionnaireSlug);
    
    Task<string> GetInitialState(string languageCode, string questionnaireSlug);
    
    Task<string> GetNextState(string thisOrigin, string languageCode, string questionnaireSlug, Dictionary<string, StringValues> formData);
    
    Task<(Stream fileStream, string contentType)> GetDecorativeImage(string questionnaireSlug);
}