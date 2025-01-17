using Newtonsoft.Json;

namespace CareLeavers.Web;

public static class Constants
{
    public static JsonSerializer Serializer { get; set; } = null!;

    public static JsonSerializerSettings SerializerSettings { get; set; } = null!;
}