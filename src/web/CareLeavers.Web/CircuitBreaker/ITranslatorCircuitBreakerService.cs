namespace CareLeavers.Web.CircuitBreaker;

public interface ITranslatorCircuitBreakerService
{
    public Task<bool> ShouldOpenCircuit(string html);
}