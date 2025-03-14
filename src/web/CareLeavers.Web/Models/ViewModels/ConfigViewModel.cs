using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Translation;

namespace CareLeavers.Web.Models.ViewModels;

public class ConfigViewModel
{
    public ContentfulConfigurationEntity ContentfulConfiguration { get; set; } = new();

    public TranslationLanguage Language { get; set; } = new();
}