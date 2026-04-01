using CareLeavers.Web.Contentful;
using Contentful.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSubstitute;

namespace CareLeavers.Web.Tests.Contentful;

public class GdsAssetJsonConverterTests
{
    private GdsAssetJsonConverter _gdsAssetJsonConverter;
    private JsonSerializer _jsonSerialiser;
    private IReferenceResolver _referenceResolver;

    [SetUp]
    public void Init()
    {
        _referenceResolver = Substitute.For<IReferenceResolver>();
        _jsonSerialiser = new JsonSerializer
        {
            ReferenceResolver = _referenceResolver
        };

        _gdsAssetJsonConverter = new GdsAssetJsonConverter();
    }

    [Test]
    public void CanConvert_Returns_True_ForAssetType()
    {
        Assert.That(_gdsAssetJsonConverter.CanConvert(typeof(Asset)), Is.True);
    }

    [Test]
    public void CanConvert_Returns_False_ForOtherTypes()
    {
        Assert.That(_gdsAssetJsonConverter.CanConvert(typeof(string)), Is.False);
    }

    [Test]
    public void ReadJson_WhenTokenTypeIsNull_Returns_Null()
    {
        const string json = "null";
        JsonTextReader reader = new(new StringReader(json));
        reader.Read(); // Move to null token

        object? result = _gdsAssetJsonConverter.ReadJson(reader, typeof(Asset), null, _jsonSerialiser);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ReadJson_WhenJsonHasRef_Returns_ResolvedReference()
    {
        const string json = "{\"$ref\": \"123\"}";
        JsonTextReader reader = new(new StringReader(json));
        Asset expectedAsset = new Asset { SystemProperties = new SystemProperties { Id = "123" } };
        _referenceResolver.ResolveReference(_jsonSerialiser, "123").Returns(expectedAsset);

        object? result = _gdsAssetJsonConverter.ReadJson(reader, typeof(Asset), null, _jsonSerialiser);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.SameAs(expectedAsset));
        _referenceResolver.Received(1).ResolveReference(_jsonSerialiser, "123");
    }

    [Test]
    public void ReadJson_DeserializesAssetWithFields_And_LocaleSet()
    {
        const string json = """
                            {
                                "sys": { "id": "asset-one", "type": "Asset", "locale": "en-US" },
                                "fields": {
                                    "title": "Title",
                                    "description": "Description",
                                    "file": { "fileName": "test.png", "contentType": "image/png", "url": "https://localhost:1234/test.png" }
                                    }
                            }
                            """;

        JsonTextReader reader = new(new StringReader(json));

        Asset? result = (Asset?)_gdsAssetJsonConverter.ReadJson(reader, typeof(Asset), null, _jsonSerialiser);

        Assert.That(result, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.SystemProperties.Id, Is.EqualTo("asset-one"));
            Assert.That(result.Title, Is.EqualTo("Title"));
            Assert.That(result.Description, Is.EqualTo("Description"));
            Assert.That(result.File.FileName, Is.EqualTo("test.png"));
            Assert.That(result.TitleLocalized["en-US"], Is.EqualTo("Title"));
            Assert.That(result.DescriptionLocalized["en-US"], Is.EqualTo("Description"));
            Assert.That(result.FilesLocalized["en-US"].FileName, Is.EqualTo("test.png"));
        }
    }

    [Test]
    public void ReadJson_DeserializesAssetWithFields_And_LocaleNotSet()
    {
        const string json = """
                            {
                                "sys": { "id": "asset-one", "type": "Asset" },
                                "fields": {
                                    "title": { "en-US": "Title EN", "sv": "Title SV" },
                                    "description": { "en-US": "Desc EN", "sv": "Desc SV" },
                                    "file": { 
                                        "en-US": { "fileName": "test_en.png" },
                                        "sv": { "fileName": "test_sv.png" }
                                        }
                                    }
                            }
                            """;
        JsonTextReader reader = new(new StringReader(json));

        Asset? result = (Asset?)_gdsAssetJsonConverter.ReadJson(reader, typeof(Asset), null, _jsonSerialiser);

        Assert.That(result, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.SystemProperties.Id, Is.EqualTo("asset-one"));
            Assert.That(result.TitleLocalized["en-US"], Is.EqualTo("Title EN"));
            Assert.That(result.TitleLocalized["sv"], Is.EqualTo("Title SV"));
            Assert.That(result.DescriptionLocalized["en-US"], Is.EqualTo("Desc EN"));
            Assert.That(result.FilesLocalized["en-US"].FileName, Is.EqualTo("test_en.png"));
        }
    }

    [Test]
    public void ReadJson_DeserializesAssetWithoutFields_And_LocaleSet()
    {
        const string json = """
                            {
                                "sys": { "id": "asset-one", "locale": "en-US" },
                                "title": "Title",
                                "description": "Description",
                                "file": { "fileName": "test.png" }
                            }
                            """;
        JsonTextReader reader = new(new StringReader(json));

        Asset? result = (Asset?)_gdsAssetJsonConverter.ReadJson(reader, typeof(Asset), null, _jsonSerialiser);

        Assert.That(result, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.Title, Is.EqualTo("Title"));
            Assert.That(result.TitleLocalized["en-US"], Is.EqualTo("Title"));
        }
    }

    [Test]
    public void ReadJson_DeserializesAssetWithoutFields_And_LocaleNotSet()
    {
        const string json = """
                            {
                                "sys": { "id": "asset-one" },
                                "title": { "en-US": "Title EN" },
                                "description": { "en-US": "Desc EN" },
                                "file": { "en-US": { "fileName": "test_en.png" } }
                            }
                            """;
        JsonTextReader reader = new(new StringReader(json));

        Asset? result = (Asset?)_gdsAssetJsonConverter.ReadJson(reader, typeof(Asset), null, _jsonSerialiser);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.TitleLocalized["en-US"], Is.EqualTo("Title EN"));
    }

    [Test]
    public void ReadJson_WhenNotAlreadyReferenced_AddsToReferenceResolver()
    {
        const string json = """{ "sys": { "id": "asset-one" } }""";
        JsonTextReader reader = new(new StringReader(json));
        _referenceResolver.ResolveReference(_jsonSerialiser, "asset-one").Returns(null);
        _referenceResolver.IsReferenced(_jsonSerialiser, Arg.Any<Asset>()).Returns(false);

        Asset? result = (Asset?)_gdsAssetJsonConverter.ReadJson(reader, typeof(Asset), null, _jsonSerialiser);

        Assert.That(result, Is.Not.Null);
        _referenceResolver.Received(1).AddReference(_jsonSerialiser, "asset-one", result);
    }

    [Test]
    public void ReadJson_WhenAlreadyReferenced_Returns_ExistingFromReferenceResolver()
    {
        const string json = """{ "sys": { "id": "asset-one" } }""";
        JsonTextReader reader = new(new StringReader(json));
        Asset existingAsset = new Asset { SystemProperties = new SystemProperties { Id = "asset-one" } };
        _referenceResolver.ResolveReference(_jsonSerialiser, "asset-one").Returns(existingAsset);

        object? result = _gdsAssetJsonConverter.ReadJson(reader, typeof(Asset), null, _jsonSerialiser);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.SameAs(existingAsset));
    }

    [Test]
    public void ReadJson_DeserializesMetadata_IfPresent()
    {
        const string json = """
                            {
                                "sys": { "id": "asset-one" },
                                "metadata": { "tags": [ { "sys": { "type": "Link", "linkType": "Tag", "id": "tag-one" } } ] }
                            }
                            """;
        JsonTextReader reader = new(new StringReader(json));

        Asset? result = (Asset?)_gdsAssetJsonConverter.ReadJson(reader, typeof(Asset), null, _jsonSerialiser);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Metadata, Is.Not.Null);
        Assert.That(result.Metadata.Tags, Is.Not.Null);
        Assert.That(result.Metadata.Tags, Has.Count.EqualTo(1));
    }
}