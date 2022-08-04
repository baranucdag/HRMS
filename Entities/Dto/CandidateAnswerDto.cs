using Entities.Abstract;

namespace Entities.Dto
{
    public class CandidateAnswerDto : IDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string CandidateFirstName { get; set; }
        public string CandidateLastName { get; set; }
        public string CandidateFullName { get; set; }
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public int AnswerValue { get; set; }
        public int IsDeleted { get; set; }
    }
}
