using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

public class RobotsController : Controller
{
    [Route("/robots.txt")]
    public async Task<IActionResult> Robots()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("User-agent: *");
        sb.AppendLine("Disallow: /translation");
        sb.AppendLine("Allow: /");
        sb.AppendLine();
        sb.AppendLine($"Sitemap: {Url.ActionLink("Sitemap", "Sitemap")}");
        
        return Content(sb.ToString(), "text/plain");
    }
}