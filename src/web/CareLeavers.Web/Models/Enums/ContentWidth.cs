using System.Runtime.Serialization;

namespace CareLeavers.Web.Models.Enums;

public enum ContentWidth
{
    [EnumMember(Value = "One Third")]
    OneThird,
    [EnumMember(Value = "Two Thirds")]
    TwoThirds,
    [EnumMember(Value = "Full Width")]
    FullWidth
}