namespace CareLeavers.Integration.Tests.Tests.SnapshotTests;

public class SnapshotTestCase : TestCaseData
{
    public SnapshotTestCase(string folder) : base(folder)
    {
        
    }
    
    public string Folder => Arguments[0] as string ?? string.Empty;
}