using System.Runtime.Serialization;

namespace CareLeavers.Web.Models.Enums;

public enum GridType
{
    Cards,
    [EnumMember(Value = "Alternating Image and Text")]
    AlternatingImageAndText,
    [EnumMember(Value = "External Links")]
    ExternalLinks,
    Banner,
    [EnumMember(Value = "Small Banner")]
    SmallBanner
}