using Entities.Abstract;

namespace Entities.Concrete
{
    public class CandidateExperience : BaseEntity, IEntity
    {
        public int CandidateId { get; set; }
        public string WorkedTime { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public string ReasonForLeaving { get; set; }
    }
}
