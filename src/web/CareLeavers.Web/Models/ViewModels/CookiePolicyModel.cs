using Azure;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.Interface;

namespace CareLeavers.Web.Models.ViewModels;

public class CookiePolicyModel : IPageViewModel
{
    public bool AcceptCookies { get; set; }
    
    public bool ShowSuccessBanner { get; set; }
    
    public Page? Page { get; set; }
}