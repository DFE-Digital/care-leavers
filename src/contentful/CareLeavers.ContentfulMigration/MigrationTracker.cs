using Contentful.Core.Models;

namespace CareLeavers.ContentfulMigration;

public class MigrationTracker : IContent
{
    public string Name { get; set; } = string.Empty;
    
    public List<Migration> Migrations { get; set; } = [];
    
    public SystemProperties? Sys { get; set; }
}