using Azure;
using CareLeavers.Web.Models.Content;

namespace CareLeavers.Web.Models.ViewModels;

public class CookiePolicyModel
{
    public bool AcceptCookies { get; set; }
    
    public bool ShowSuccessBanner { get; set; }
    
    public Page? Page { get; set; }
}