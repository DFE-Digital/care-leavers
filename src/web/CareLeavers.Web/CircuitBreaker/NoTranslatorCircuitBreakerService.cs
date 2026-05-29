namespace CareLeavers.Web.CircuitBreaker;

public class NoTranslatorCircuitBreakerService : ITranslatorCircuitBreakerService
{
    public Task<bool> ShouldOpenCircuit(string html) => Task.FromResult(true);
}