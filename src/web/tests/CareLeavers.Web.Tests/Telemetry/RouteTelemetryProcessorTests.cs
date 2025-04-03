using System.Diagnostics;
using CareLeavers.Web.Telemetry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NSubstitute;

namespace CareLeavers.Web.Tests.Telemetry;

public class RouteTelemetryProcessorTests
{
    [Test]
    public void WhenSlugIsInPathThenIsReplacedFromRouteData()
    {
        // Arrange
        var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        var httpContext = new DefaultHttpContext();
        var routeData = new RouteValueDictionary
        {
            { "slug", "test-slug" }
        };
        //routeData.Values.Add("slug", "test-slug");
        httpContext.Request.RouteValues = routeData;
        httpContextAccessor.HttpContext.Returns(httpContext);
        
        var processor = new RouteTelemetryProcessor(httpContextAccessor);
        var activity = new Activity("test");

        // Act
        processor.OnEnd(activity);

        // Assert
        Assert.That(activity.Tags.Single(x => x.Key == "http.route").Value, Is.EqualTo("/en/test-slug"));
    }
    
    [Test]
    public void WhenSlugAndLanguageIsInPathThenIsReplacedFromRouteData()
    {
        // Arrange
        var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        var httpContext = new DefaultHttpContext();
        var routeData = new RouteValueDictionary
        {
            { "slug", "test-slug" },
            { "languageCode", "pl" }
        };
        httpContext.Request.RouteValues = routeData;
        httpContextAccessor.HttpContext.Returns(httpContext);
        
        var processor = new RouteTelemetryProcessor(httpContextAccessor);
        var activity = new Activity("test");

        // Act
        processor.OnEnd(activity);

        // Assert
        Assert.That(activity.Tags.Single(x => x.Key == "http.route").Value, Is.EqualTo("/pl/test-slug"));
    }
    
    [Test]
    public void WhenSlugIsNotInPathThenIsNotReplacedFromRouteData()
    {
        // Arrange
        var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        var httpContext = new DefaultHttpContext();
        httpContextAccessor.HttpContext.Returns(httpContext);
        
        var processor = new RouteTelemetryProcessor(httpContextAccessor);
        var activity = new Activity("test");

        // Act
        processor.OnEnd(activity);

        // Assert
        Assert.That(activity.Tags.Any(x => x.Key == "http.route"), Is.False);
    }
    
    [Test]
    public void NullHttpContextDoesNotThrow()
    {
        // Arrange
        var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        httpContextAccessor.HttpContext.Returns((HttpContext)null!);
        
        var processor = new RouteTelemetryProcessor(httpContextAccessor);
        var activity = new Activity("test");

        // Act
        processor.OnEnd(activity);

        // Assert
        Assert.Pass();
    }
}