using Entities.Abstract;

namespace Entities.Dto
{
    public class CandidateComputerExperienceDto : IDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string CandidateFirstName { get; set; }
        public string CandidateLastName { get; set; }
        public string CandidateFullName { get; set; }
        public string ExperienceType { get; set; }
        public string LanguageOrProgram { get; set; }
        public string WorksDone { get; set; }
        public string Duration { get; set; }
        public int IsDeleted { get; set; }
    }
}
