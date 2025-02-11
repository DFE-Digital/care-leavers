using System.Diagnostics;
using System.Reflection;
using CareLeavers.ContentfulMigration;
using Contentful.Core;
using Contentful.Core.Configuration;
using Contentful.Core.Errors;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Microsoft.Extensions.Configuration;

var configBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
    .AddEnvironmentVariables();

var config = configBuilder.Build();

var contentfulOptions = new ContentfulOptions();
config.GetSection("ContentfulOptions").Bind(contentfulOptions);

if (string.IsNullOrEmpty(contentfulOptions.SpaceId) || 
    string.IsNullOrEmpty(contentfulOptions.ManagementApiKey) ||
    string.IsNullOrEmpty(contentfulOptions.DeliveryApiKey) ||
    string.IsNullOrEmpty(contentfulOptions.Environment))
{
    Console.WriteLine("Contentful configuration not found. Please ensure that the configuration is set.");
    return -1;
}

var httpClient = new HttpClient();
var managementClient = new ContentfulManagementClient(httpClient, contentfulOptions);
var contentClient = new ContentfulClient(httpClient, contentfulOptions);

Console.WriteLine("Verifying migration tracker content type.");

try
{
    await managementClient.GetContentType("migrationTracker");
}
catch (ContentfulException)
{
    Console.WriteLine("Migration tracker content type not found. Creating now.");

    var resp = await managementClient.CreateOrUpdateContentType(new ContentType
    {
        SystemProperties = new SystemProperties()
        {
            Id = "migrationTracker",
        },
        Name = "Migration Tracker",
        Description = "Tracks applied contentful migrations",
        DisplayField = "migrations",
        Fields =
        [
            new Field
            {
                Id = "migrations",
                Name = "Migrations",
                Type = "Object"
            }
        ]
    });
    
    await managementClient.ActivateContentType(
        resp.SystemProperties.Id, 
        resp.SystemProperties.Version ?? 1);
}

Console.WriteLine("Verifying migration content exists");

var query = new QueryBuilder<MigrationTracker>()
    .ContentTypeIs("migrationTracker")
    .Include(0);

var existingMigrationTrackers = (await contentClient.GetEntries(query)).ToList();

if (existingMigrationTrackers.Count > 1)
{
    Console.WriteLine("Multiple migration tracker content found. Please ensure that only one migration tracker content exists.");
    return -1;
}

if (!existingMigrationTrackers.Any())
{
    Console.WriteLine("Migration tracker content not found. Creating now.");

    var newEntry = new Entry<dynamic>
    {
        SystemProperties = new SystemProperties(),
        Fields = new
        {
            migrations = new List<Migration>()
        }
    };
    
    var resp = await managementClient.CreateEntry(newEntry, "migrationTracker");
    
    await managementClient.PublishEntry(resp.SystemProperties.Id, resp.SystemProperties.Version ?? 1);
    
    await Task.Delay(2000);
    
    existingMigrationTrackers = (await contentClient.GetEntries(query)).ToList();
}

var migrationTracker = existingMigrationTrackers.First();

var migrationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations");

var migrationFiles = Directory.GetFiles(migrationPath, "*.cjs", SearchOption.TopDirectoryOnly)
    .Select(Path.GetFileNameWithoutExtension)
    .OrderBy(x => x)
    .ToList();

// Scan for migration files

var anyMigrationsHaveApplied = false;

foreach (var migrationFile in migrationFiles)
{
    var existingMigration = migrationTracker.Migrations.SingleOrDefault(x => x.Name == migrationFile);
    
    if (existingMigration != null)
    {
        Console.WriteLine($"Migration {migrationFile} already applied.");
        continue;
    }
    
    Console.WriteLine($"Applying migration {migrationFile}.");

    var success = await RunContentfulCommand(
        $"space migration --space-id \"{contentfulOptions.SpaceId}\" " +
        $"--environment-id \"{contentfulOptions.Environment}\" " +
        $"--management-token \"{contentfulOptions.ManagementApiKey}\" " +
        $"\"Migrations/{migrationFile}.cjs\" --yes");
    
    migrationTracker.Migrations.Add(new Migration
    {
        Name = migrationFile ?? string.Empty,
        AppliedAtUtc = DateTime.UtcNow,
        Success = success
    });
    
    anyMigrationsHaveApplied = true;
}

if (anyMigrationsHaveApplied)
{
    Console.WriteLine("Updating migration tracker content.");
    
    var updatedEntry = new Entry<dynamic>
    {
        SystemProperties = new SystemProperties()
        {
            Id = migrationTracker.Sys?.Id,
            Version = migrationTracker.Sys?.Version
        },
        Fields = new
        {
            migrations = new Dictionary<string, dynamic>
            {
                ["en-US"] = migrationTracker.Migrations
            }
        }
    };

    var updatedEntryResp =
        await managementClient.CreateOrUpdateEntry(updatedEntry, version: migrationTracker.Sys?.PublishedVersion + 1);
    await managementClient.PublishEntry(updatedEntry.SystemProperties.Id,
        (updatedEntryResp.SystemProperties.Version ?? 1));
}

async Task<bool> RunContentfulCommand(string args)
{
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "contentful",
            Arguments = args,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        }
    };
    process.Start();

    while (!process.StandardOutput.EndOfStream)
    {
        Console.WriteLine(process.StandardOutput.ReadLine());
    }

    await process.WaitForExitAsync();
    
    return process.ExitCode == 0;
}

return 0;