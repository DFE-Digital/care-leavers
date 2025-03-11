namespace CareLeavers.Integration.Tests.Tests.SnapshotTests;

public class SnapshotTestCase(string folder) : TestCaseData(folder)
{
    public string Folder => Arguments[0] as string ?? string.Empty;
}