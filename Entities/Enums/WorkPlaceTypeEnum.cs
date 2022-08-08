using System.ComponentModel;

namespace Entities.Enums
{
    public enum WorkPlaceTypeEnum
    {
        [Description("Remote")]
        Remote = 0,

        [Description("Hybrid")]
        Hybrid = 1,

        [Description("From Ofice")]
        FromOffice=2
    }
}
