using Entities.Abstract;

namespace Entities.Concrete
{
    public class CandidateComputerExperience : BaseEntity, IEntity
    {
        public int CandidateId { get; set; }
        public string ExperienceType { get; set; }
        public string LanguageOrProgram { get; set; }
        public string WorksDone { get; set; }
        public string Duration { get; set; }
    }
}
