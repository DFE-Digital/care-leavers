using System.Runtime.Serialization;

namespace CareLeavers.Web.Models.Enums;

public enum PageWidth
{
    [EnumMember(Value = "Two Thirds")]
    TwoThirds,
    [EnumMember(Value = "Full Width")]
    FullWidth
}