using System.Runtime.Serialization;

namespace CareLeavers.Web.Models.Enums;

public enum GridType
{
    Cards,
    [EnumMember(Value = "Alternating Image and Text")]
    AlternatingImageAndText,
    [EnumMember(Value = "External Links")]
    ExternalLinks,
    [Obsolete("No longer used, use Banner entity instead")]
    Banner,
    [Obsolete("No longer used, use Banner entity instead")]
    [EnumMember(Value = "Small Banner")]
    SmallBanner
}