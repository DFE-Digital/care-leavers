using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Tests;

public sealed class MockSession : ISession
{
    private readonly Dictionary<string, byte[]> _sessionStorage = new();

    public Task LoadAsync(CancellationToken cancellationToken = new()) => Task.CompletedTask;

    public Task CommitAsync(CancellationToken cancellationToken = new()) => Task.CompletedTask;

    public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value) =>
        _sessionStorage.TryGetValue(key, out value);

    public void Set(string key, byte[] value) => _sessionStorage[key] = value;

    public void Remove(string key) => _sessionStorage.Remove(key);

    public void Clear() => _sessionStorage.Clear();

    public bool IsAvailable => true;
    public string Id { get; } = Guid.NewGuid().ToString();
    public IEnumerable<string> Keys => _sessionStorage.Keys;
}