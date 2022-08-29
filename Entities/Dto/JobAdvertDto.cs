using Entities.Abstract;
using System;

namespace Entities.Dto
{
    public class JobAdvertDto : IDto
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public string QualificationLevel { get; set; }
        public string DepartmentText { get; set; }
        public int Department { get; set; }
        public string WorkPlaceTypeText { get; set; }
        public int WorkPlaceType { get; set; }
        public string WorkTimeTypeText { get; set; }
        public int WorkTimeType { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int IsDeleted { get; set; }
        public string IsDeletedText => (IsDeleted == 1) ? "yes" : "no";
    }
}
