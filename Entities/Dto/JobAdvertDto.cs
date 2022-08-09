using Entites.Enums;
using Entities.Abstract;
using Entities.Enums;
using System;

namespace Entities.Dto
{
    public class JobAdvertDto : IDto
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public string QualificationLevel { get; set; }
        public string WorkPlaceType { get; set; }
        public string WorkTimeType { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int IsDeleted { get; set; }
        public string IsDeletedText => (IsDeleted == 1) ? "yes" : "no";
    }
}
