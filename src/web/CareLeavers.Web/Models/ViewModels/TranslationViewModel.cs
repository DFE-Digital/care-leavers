using CareLeavers.Web.Translation;

namespace CareLeavers.Web.Models.ViewModels;

public class TranslationViewModel
{
    public IEnumerable<TranslationLanguage> Languages { get; set; } = [];
    
    public string? Slug { get; set; }
}