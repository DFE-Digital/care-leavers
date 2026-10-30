using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.Interface;

namespace CareLeavers.Web.Models.ViewModels
{
    public class SharedViewModel : IPageViewModel
    {
        public Page? Page { get; set; }

        public CookiePolicyModel? CookiePolicyModel { get; set; }
        public FeedbackViewModel? FeedbackViewModel { get; set; }
    }
}
