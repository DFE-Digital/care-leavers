namespace CareLeavers.Web.Session;

public sealed class SessionUsageService(IHttpContextAccessor accessor)
{
    public void IncrementUsage(SessionState key)
    {
        if (accessor.HttpContext is null)
            throw new InvalidOperationException("TODO"); // TODO: Message;

        accessor.HttpContext.Session.SetInt32(key.ToString(), GetUsage(key) + 1);
    }

    public int GetUsage(SessionState key)
    {
        if (accessor.HttpContext is null)
            throw new InvalidOperationException("TODO"); // TODO: Message

        return accessor.HttpContext.Session.GetInt32(key.ToString()) ?? 0;
    }
}