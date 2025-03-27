using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Translation;

namespace CareLeavers.Web.Models.ViewModels;

public class ConfigViewModel
{
    public ContentfulConfigurationEntity ContentfulConfiguration { get; set; } = new();

    public TranslationLanguage Language { get; set; } = new();

    public bool IsError { get; set; } = false;

    public bool ShowLanguage { get; set; } = true;

    public bool ShowNavigation { get; set; } = true;

    public bool ShowFooterLinks { get; set; } = true;
}