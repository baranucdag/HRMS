using System.ComponentModel;

namespace Entities.Enums
{
    public enum ApplicationStatusEnum
    {
        [Description("FirstStep")]
        Applied = 0,

        [Description("SecondStep")]
        Proved = 1,

        [Description("Offered")]
        Offered = 2
    }
}
