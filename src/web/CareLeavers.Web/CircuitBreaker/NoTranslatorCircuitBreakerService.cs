namespace CareLeavers.Web.CircuitBreaker;

public sealed class NoTranslatorCircuitBreakerService : ITranslatorCircuitBreakerService
{
    public Task<bool> ShouldOpenCircuit(string html) => Task.FromResult(true);
}