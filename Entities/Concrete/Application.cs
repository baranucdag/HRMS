using Entities.Abstract;
using Entities.Enums;
using System;

namespace Entities.Concrete
{
    public class Application : BaseEntity, IEntity
    {
        public int JobAdvertId { get; set; }
        public int CandidateId { get; set; }
        public ApplicationStatusEnum ApplicationStatus { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
