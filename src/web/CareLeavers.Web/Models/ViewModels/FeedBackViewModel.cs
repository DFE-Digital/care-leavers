using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Translation;

namespace CareLeavers.Web.Models.ViewModels;

public class FeedbackViewModel
{
    public string? FeedbackUrl { get; set; } = string.Empty;

    public Page? Page { get; set; }
}