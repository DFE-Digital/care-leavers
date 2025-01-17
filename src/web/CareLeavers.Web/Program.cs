using CareLeavers.Web.ContentfulRenderers;
using Contentful.AspNetCore;
using Contentful.Core.Models;
using GovUk.Frontend.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddGovUkFrontend();

builder.Services.AddContentful(builder.Configuration);

builder.Services.AddTransient<HtmlRenderer>((c) => {
    var renderer = new HtmlRenderer(new HtmlRendererOptions
    {
        ListItemOptions = new ListItemContentRendererOptions
        {
            OmitParagraphTagsInsideListItems = true
        }
    });
    
    // Add custom GDS renderer
    renderer.AddRenderer(new GDSParagraphRenderer(renderer.Renderers));
    renderer.AddRenderer(new GDSHeaderRenderer(renderer.Renderers));
    
    return renderer;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program
{
}