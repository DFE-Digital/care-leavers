using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;

namespace CareLeavers.Web.Tests.Contentful;

public class ContentfulEntityResolverTests
{
    private ContentfulEntityResolver _contentfulEntityResolver;

    [SetUp]
    public void Init()
    {
        _contentfulEntityResolver = new ContentfulEntityResolver();
    }
    
    private static IEnumerable<TestCaseData> ContentTypeTestData()
    {
        yield return new TestCaseData(Page.ContentType, typeof(Page));
        yield return new TestCaseData(Grid.ContentType, typeof(Grid));
        yield return new TestCaseData(Card.ContentType, typeof(Card));
        yield return new TestCaseData(ExternalAgency.ContentType, typeof(ExternalAgency));
        yield return new TestCaseData(RichContentBlock.ContentType, typeof(RichContentBlock));
        yield return new TestCaseData(RichContent.ContentType, typeof(RichContent));
        yield return new TestCaseData(AnswerEntity.ContentType, typeof(AnswerEntity));
        yield return new TestCaseData(Riddle.ContentType, typeof(Riddle));
        yield return new TestCaseData(GetToAnAnswer.ContentType, typeof(GetToAnAnswer));
        yield return new TestCaseData(StatusChecker.ContentType, typeof(StatusChecker));
        yield return new TestCaseData(Banner.ContentType, typeof(Banner));
        yield return new TestCaseData(DefinitionLink.ContentType, typeof(DefinitionLink));
        yield return new TestCaseData(Definition.ContentType, typeof(Definition));
        yield return new TestCaseData(Spacer.ContentType, typeof(Spacer));
        yield return new TestCaseData(PrintableCollection.ContentType, typeof(PrintableCollection));
        yield return new TestCaseData(CallToAction.ContentType, typeof(CallToAction));
        yield return new TestCaseData(Button.ContentType, typeof(Button));
    }

    [TestCaseSource(nameof(ContentTypeTestData))]
    public void Resolve_Returns_Correct_Type(string contentTypeId, Type expectedType)
    {
        Type? result = _contentfulEntityResolver.Resolve(contentTypeId);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(expectedType));
    }

    [Test]
    public void Resolve_Returns_Null_For_Unknown_ContentType()
    {
        Type? result = _contentfulEntityResolver.Resolve("unknown-content-type");
        
        Assert.That(result, Is.Null);
    }
}
