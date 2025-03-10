using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace CareLeavers.Web.Filters;

public class ContentfulCaching : ActionFilterAttribute
{
    private MemoryStream? _memoryStream;
    private Stream? _originalBodyStream;
    
    public override async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        _memoryStream = null;
        _originalBodyStream = null;
        
        var distributedCache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();
        
        var slug = context.RouteData.Values["slug"]?.ToString();
        
        if (slug == null)
        {
            await base.OnActionExecutionAsync(context, next);
            return;
        }
        
        var cachedResponse = await distributedCache.GetAsync($"contentful:html:{slug}");
        
        if (cachedResponse != null)
        {
            context.Result = new ContentResult
            {
                Content = Encoding.UTF8.GetString(cachedResponse),
                ContentType = "text/html"
            };
            
            return;
        }
        
        _memoryStream = new MemoryStream();
        _originalBodyStream = context.HttpContext.Response.Body;
        context.HttpContext.Response.Body = _memoryStream;
        
        await base.OnActionExecutionAsync(context, next);
    }

    public override async Task OnResultExecutionAsync(
        ResultExecutingContext context,
        ResultExecutionDelegate next)
    {
        await base.OnResultExecutionAsync(context, next);
        
        var slug = context.RouteData.Values["slug"]?.ToString();
        
        if (slug == null || _memoryStream == null || _originalBodyStream == null)
        {
            return;
        }
        
        var distributedCache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();
        
        _memoryStream.Seek(0, SeekOrigin.Begin);
        
        var responseBody = await new StreamReader(_memoryStream).ReadToEndAsync();
        
        _memoryStream.Seek(0, SeekOrigin.Begin);
        
        context.HttpContext.Response.Body = _originalBodyStream!;
        await context.HttpContext.Response.Body!.WriteAsync(_memoryStream.ToArray());
        
        await distributedCache.SetAsync($"contentful:html:{slug}", Encoding.UTF8.GetBytes(responseBody));
    }
}