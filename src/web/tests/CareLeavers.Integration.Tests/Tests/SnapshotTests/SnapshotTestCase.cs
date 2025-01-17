namespace CareLeavers.Integration.Tests.Tests.SnapshotTests;

public class SnapshotTestCase : TestCaseData
{
    public SnapshotTestCase(string fileName) : base(fileName)
    {
        
    }
    
    public string FileName => Arguments[0] as string ?? string.Empty;
}