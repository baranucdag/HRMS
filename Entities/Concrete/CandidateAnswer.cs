using Entities.Abstract;

namespace Entities.Concrete
{
    public class CandidateAnswer : IEntity
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int AnswerId { get; set; }
        public int AnswerValue { get; set; }
        public int IsDeleted { get; set; }
    }
}
    