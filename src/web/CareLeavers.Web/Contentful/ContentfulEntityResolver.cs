using CareLeavers.Web.Models.Content;
using Contentful.Core.Configuration;

namespace CareLeavers.Web.Contentful;

public class ContentfulEntityResolver : IContentTypeResolver
{
    private readonly Dictionary<string, Type> _types = new()
    {
        { Page.ContentType, typeof(Page) },
        { Grid.ContentType, typeof(Grid) },
        { Card.ContentType, typeof(Card) },
        { RichContent.ContentType, typeof(RichContent) },
        { AnswerEntity.ContentType, typeof(AnswerEntity) },
        { Riddle.ContentType, typeof(Riddle) },
        { StatusChecker.ContentType, typeof(StatusChecker) },
    };
    
    public Type? Resolve(string contentTypeId)
    {
        return _types.TryGetValue(contentTypeId, out var type) ? type : null;
    }
}