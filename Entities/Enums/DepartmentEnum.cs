using System.ComponentModel;

namespace Entities.Enums
{
    public enum DepartmentEnum
    {
        [Description("System")]
        System = 0,

        [Description("Software")]
        software = 1,

        [Description("Human Resources")]
        HumanResources = 2
    }
}
