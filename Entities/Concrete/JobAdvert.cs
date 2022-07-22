using Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class JobAdvert : IEntity
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public string QualificationLevel { get; set; }
        public string WorkType { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime DeadLine { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
