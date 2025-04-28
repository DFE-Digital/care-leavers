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
        { ExternalAgency.ContentType, typeof(ExternalAgency) },
        { RichContentBlock.ContentType, typeof(RichContentBlock) },
        { RichContent.ContentType, typeof(RichContent) },
        { AnswerEntity.ContentType, typeof(AnswerEntity) },
        { Riddle.ContentType, typeof(Riddle) },
        { StatusChecker.ContentType, typeof(StatusChecker) },
        { Banner.ContentType, typeof(Banner) },
        { DefinitionLink.ContentType, typeof(DefinitionLink) },
        { Definition.ContentType, typeof(Definition) },
        { Spacer.ContentType, typeof(Spacer) },
        { PrintableCollection.ContentType, typeof(PrintableCollection) }
    };
    
    public Type? Resolve(string contentTypeId)
    {
        return _types.TryGetValue(contentTypeId, out var type) ? type : null;
    }
}