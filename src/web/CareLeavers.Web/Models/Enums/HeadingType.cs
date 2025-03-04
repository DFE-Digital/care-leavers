namespace CareLeavers.Web.Models.Enums;

public enum HeadingType
{
    [Obsolete("Only use H2 and below", true)]
    H1,
    H2,
    H3,
    H4,
    H5,
    H6
}