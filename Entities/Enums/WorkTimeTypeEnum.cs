using System.ComponentModel;

namespace Entites.Enums
{
    public enum WorkTimeTypeEnum
    {
        [Description("Full-Time")]
        PartTime=0,

        [Description("Part-Time")]
         FullTime =1,

        [Description("Intern")]
        Intern = 2
    }
}
