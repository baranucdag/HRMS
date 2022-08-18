using Entites.Enums;
using Entities.Abstract;
using Entities.Enums;
using System;

namespace Entities.Concrete
{
    public class JobAdvert : IEntity
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public string QualificationLevel { get; set; }
        public DepartmentEnum Department { get; set; }
        public WorkPlaceTypeEnum WorkPlaceType { get; set; }
        public WorkTimeTypeEnum WorkTimeType { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int IsDeleted { get; set; }
    }
}
