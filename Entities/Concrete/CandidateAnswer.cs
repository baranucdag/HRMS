using Entities.Abstract;

namespace Entities.Concrete
{
    public class CandidateAnswer : BaseEntity, IEntity
    {
        public int CandidateId { get; set; }
        public int AnswerId { get; set; }
        public int AnswerValue { get; set; }
    }
}
    