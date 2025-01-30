using Joonasw.AspNetCore.SecurityHeaders.Csp;

namespace CareLeavers.Integration.Tests.TestSupport;

public class MockCspNonceService : ICspNonceService
{
    public string GetNonce()
    {
        return "mock-nonce";
    }
}