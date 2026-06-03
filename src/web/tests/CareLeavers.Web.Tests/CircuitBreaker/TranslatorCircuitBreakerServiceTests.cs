using System.Text.Json;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CareLeavers.Web.CircuitBreaker;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Translation;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace CareLeavers.Web.Tests.CircuitBreaker;

public sealed class TranslatorCircuitBreakerServiceTests
{
    private BlobServiceClient _blobServiceClient;
    private IOptions<BlobStorageOptions> _blobStorageOptions;
    private IOptions<AzureTranslationOptions> _azureTranslationOptions;

    private BlobClient _blobClient;
    private TranslatorCircuitBreakerService _translatorCircuitBreakerService;

    private static readonly BlobContentInfo BlobContentInfo =
        BlobsModelFactory.BlobContentInfo(default, default, null, null, 0);

    [SetUp]
    public void Init()
    {
        BlobContainerClient blobContainerClient = Substitute.For<BlobContainerClient>();
        _blobClient = Substitute.For<BlobClient>();

        _blobServiceClient = Substitute.For<BlobServiceClient>();
        _blobStorageOptions = Options.Create(new BlobStorageOptions
            { ContainerName = "test-container", BlobName = "test-blob" });
        _azureTranslationOptions = Options.Create(new AzureTranslationOptions { CharacterLimit = int.MaxValue });

        _blobServiceClient.GetBlobContainerClient(Arg.Any<string>()).Returns(blobContainerClient);
        blobContainerClient.GetBlobClient(Arg.Any<string>()).Returns(_blobClient);

        _blobClient.ExistsAsync(CancellationToken.None)
            .Returns(Response.FromValue(true, Substitute.For<Response>()));

        _translatorCircuitBreakerService =
            new TranslatorCircuitBreakerService(_blobServiceClient, _blobStorageOptions, _azureTranslationOptions);
    }

    [Test]
    public async Task ShouldOpenCircuit_Is_Open_When_CharacterCount_IsGreaterThan_CharacterLimit()
    {
        _azureTranslationOptions = Options.Create(new AzureTranslationOptions { CharacterLimit = 0 });
        _translatorCircuitBreakerService =
            new TranslatorCircuitBreakerService(_blobServiceClient, _blobStorageOptions, _azureTranslationOptions);
        TranslatorCircuitBreakerData data = new() { CharacterCount = 128, TimeStamp = DateTime.UtcNow };

        _blobClient.DownloadToAsync(Arg.Do<Stream>(x =>
        {
            using StreamWriter writer = new(x, leaveOpen: true);
            writer.Write(JsonSerializer.Serialize(data, JsonSerializerOptions.Web));
            x.Seek(0, SeekOrigin.Begin);
        })).Returns(Task.FromResult(Substitute.For<Response>()));

        bool result = await _translatorCircuitBreakerService.ShouldOpenCircuit("<p>Test</p>");

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task ShouldOpenCircuit_Is_Closed_When_CharacterCount_IsLessThan_CharacterLimit()
    {
        const string html = "<p>Test</p>";
        TranslatorCircuitBreakerData data = new() { CharacterCount = 128, TimeStamp = DateTime.UtcNow };
        TranslatorCircuitBreakerData? expectedData = null;
        int expectedCharacterCount = data.CharacterCount + html.Length;

        _blobClient.DownloadToAsync(Arg.Do<Stream>(x =>
        {
            using StreamWriter writer = new(x, leaveOpen: true);
            writer.Write(JsonSerializer.Serialize(data, JsonSerializerOptions.Web));
            x.Seek(0, SeekOrigin.Begin);
        })).Returns(Task.FromResult(Substitute.For<Response>()));

        _blobClient.UploadAsync(Arg.Do<Stream>(x =>
        {
            using StreamReader reader = new(x);
            expectedData =
                JsonSerializer.Deserialize<TranslatorCircuitBreakerData>(reader.ReadToEnd(), JsonSerializerOptions.Web);
        }), Arg.Any<bool>()).Returns(Task.FromResult(Response.FromValue(BlobContentInfo, Substitute.For<Response>())));

        bool result = await _translatorCircuitBreakerService.ShouldOpenCircuit(html);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(expectedData, Is.Not.Null);
        }
        Assert.That(expectedData.CharacterCount, Is.EqualTo(expectedCharacterCount));
    }

    [Test]
    public async Task ShouldOpenCircuit_Is_Reset_When_Month_IsDifferent()
    {
        const string html = "<p>Test</p>";
        TranslatorCircuitBreakerData data = new() { CharacterCount = 128, TimeStamp = DateTime.UtcNow.AddMonths(-1) };
        TranslatorCircuitBreakerData? expectedData = null;
        int expectedCharacterCount = html.Length;

        _blobClient.DownloadToAsync(Arg.Do<Stream>(x =>
        {
            using StreamWriter writer = new(x, leaveOpen: true);
            writer.Write(JsonSerializer.Serialize(data, JsonSerializerOptions.Web));
            x.Seek(0, SeekOrigin.Begin);
        })).Returns(Task.FromResult(Substitute.For<Response>()));

        _blobClient.UploadAsync(Arg.Do<Stream>(x =>
        {
            using StreamReader reader = new(x);
            expectedData =
                JsonSerializer.Deserialize<TranslatorCircuitBreakerData>(reader.ReadToEnd(), JsonSerializerOptions.Web);
        }), Arg.Any<bool>()).Returns(Task.FromResult(Response.FromValue(BlobContentInfo, Substitute.For<Response>())));

        bool result = await _translatorCircuitBreakerService.ShouldOpenCircuit(html);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(expectedData, Is.Not.Null);
        }

        using (Assert.EnterMultipleScope())
        {
            Assert.That(expectedData.CharacterCount, Is.EqualTo(expectedCharacterCount));
            Assert.That(expectedData.TimeStamp.Month, Is.EqualTo(DateTime.UtcNow.Month));
            Assert.That(expectedData.TimeStamp.Year, Is.EqualTo(DateTime.UtcNow.Year));
        }
    }
    
    [Test]
    public async Task ShouldOpenCircuit_Is_Reset_When_Year_IsDifferent()
    {
        const string html = "<p>Test</p>";
        TranslatorCircuitBreakerData data = new() { CharacterCount = 128, TimeStamp = DateTime.UtcNow.AddYears(-1) };
        TranslatorCircuitBreakerData? expectedData = null;
        int expectedCharacterCount = html.Length;

        _blobClient.DownloadToAsync(Arg.Do<Stream>(x =>
        {
            using StreamWriter writer = new(x, leaveOpen: true);
            writer.Write(JsonSerializer.Serialize(data, JsonSerializerOptions.Web));
            x.Seek(0, SeekOrigin.Begin);
        })).Returns(Task.FromResult(Substitute.For<Response>()));

        _blobClient.UploadAsync(Arg.Do<Stream>(x =>
        {
            using StreamReader reader = new(x);
            expectedData =
                JsonSerializer.Deserialize<TranslatorCircuitBreakerData>(reader.ReadToEnd(), JsonSerializerOptions.Web);
        }), Arg.Any<bool>()).Returns(Task.FromResult(Response.FromValue(BlobContentInfo, Substitute.For<Response>())));

        bool result = await _translatorCircuitBreakerService.ShouldOpenCircuit(html);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(expectedData, Is.Not.Null);
        }

        using (Assert.EnterMultipleScope())
        {
            Assert.That(expectedData.CharacterCount, Is.EqualTo(expectedCharacterCount));
            Assert.That(expectedData.TimeStamp.Month, Is.EqualTo(DateTime.UtcNow.Month));
            Assert.That(expectedData.TimeStamp.Year, Is.EqualTo(DateTime.UtcNow.Year));
        }
    }
    
    [Test]
    public async Task ShouldOpenCircuit_Is_Not_Reset_When_MonthAndYear_IsSame()
    {
        const string html = "<p>Test</p>";
        TranslatorCircuitBreakerData data = new() { CharacterCount = 128, TimeStamp = DateTime.UtcNow };
        TranslatorCircuitBreakerData? expectedData = null;
        int expectedCharacterCount = data.CharacterCount + html.Length;

        _blobClient.DownloadToAsync(Arg.Do<Stream>(x =>
        {
            using StreamWriter writer = new(x, leaveOpen: true);
            writer.Write(JsonSerializer.Serialize(data, JsonSerializerOptions.Web));
            x.Seek(0, SeekOrigin.Begin);
        })).Returns(Task.FromResult(Substitute.For<Response>()));

        _blobClient.UploadAsync(Arg.Do<Stream>(x =>
        {
            using StreamReader reader = new(x);
            expectedData =
                JsonSerializer.Deserialize<TranslatorCircuitBreakerData>(reader.ReadToEnd(), JsonSerializerOptions.Web);
        }), Arg.Any<bool>()).Returns(Task.FromResult(Response.FromValue(BlobContentInfo, Substitute.For<Response>())));

        bool result = await _translatorCircuitBreakerService.ShouldOpenCircuit(html);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(expectedData, Is.Not.Null);
        }

        using (Assert.EnterMultipleScope())
        {
            Assert.That(expectedData.CharacterCount, Is.EqualTo(expectedCharacterCount));
            Assert.That(expectedData.TimeStamp.Month, Is.EqualTo(DateTime.UtcNow.Month));
            Assert.That(expectedData.TimeStamp.Year, Is.EqualTo(DateTime.UtcNow.Year));
        }
    }
    
    [Test]
    public async Task ShouldOpenCircuit_CreatesNewBlob_When_Blob_DoesNotExist()
    {
        const string html = "<p>Test</p>";
        TranslatorCircuitBreakerData? expectedData = null;
        int expectedCharacterCount = html.Length;

        _blobClient.DownloadToAsync(Arg.Do<Stream>(x =>
        {
            using StreamWriter writer = new(x, leaveOpen: true);
        })).Returns(Task.FromResult(Substitute.For<Response>()));

        _blobClient.UploadAsync(Arg.Do<Stream>(x =>
        {
            using StreamReader reader = new(x);
            expectedData =
                JsonSerializer.Deserialize<TranslatorCircuitBreakerData>(reader.ReadToEnd(), JsonSerializerOptions.Web);
        }), Arg.Any<bool>()).Returns(Task.FromResult(Response.FromValue(BlobContentInfo, Substitute.For<Response>())));

        bool result = await _translatorCircuitBreakerService.ShouldOpenCircuit(html);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(expectedData, Is.Not.Null);
        }
        Assert.That(expectedData.CharacterCount, Is.EqualTo(expectedCharacterCount));
    }

    [Test]
    public async Task ShouldOpenCircuit_CreatesNewBlob_When_Stream_IsEmpty()
    {
        const string html = "<p>Test</p>";
        TranslatorCircuitBreakerData? expectedData = null;
        int expectedCharacterCount = html.Length;
        _blobClient.ExistsAsync(CancellationToken.None).Returns(Response.FromValue(false, Substitute.For<Response>()));

        _blobClient.UploadAsync(Arg.Do<Stream>(x =>
        {
            using StreamReader reader = new(x);
            expectedData =
                JsonSerializer.Deserialize<TranslatorCircuitBreakerData>(reader.ReadToEnd(), JsonSerializerOptions.Web);
        }), Arg.Any<bool>()).Returns(Task.FromResult(Response.FromValue(BlobContentInfo, Substitute.For<Response>())));

        bool result = await _translatorCircuitBreakerService.ShouldOpenCircuit(html);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(expectedData, Is.Not.Null);
        }
        Assert.That(expectedData.CharacterCount, Is.EqualTo(expectedCharacterCount));
    }
}