using Contentful.Core.Models;

namespace CareLeavers.ContenfulMigration;

public class MigrationTracker : IContent
{
    public List<string> Migrations { get; set; } = new();
    
    public SystemProperties? Sys { get; set; }
}