using Microsoft.AspNetCore.Mvc;
using CareLeavers.Web.Configuration;

namespace CareLeavers.Web.Controllers
{
    public class SecurityTextController : Controller
    {
        [HttpGet("security.txt")]
        [HttpGet(".well-known/security.txt")]
        [Route("SecurityText")]
        public IActionResult GetSecurityText()
        {

            var url = SiteConfiguration.SecurityTxtUrl;
            if (string.IsNullOrEmpty(url))
            {
                return NotFound("Security file was not found");
            }

            return Redirect(url);
        }
    }
}
