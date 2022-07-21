using Entities.Abstract;

namespace Entities.Concrete
{
    public class CandidateExperience : IEntity
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string ExperienceType { get; set; }
        public string LanguageOrProgram { get; set; }
        public string WorksDone { get; set; }
        public string Duration { get; set; }
    }
}
