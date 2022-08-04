using Entities.Abstract;
using System;

namespace Entities.Dto
{
    public class ApplicationDetailDto : IDto
    {
        public int Id { get; set; }
        public int JobAdvertId { get; set; }
        public string PositionName { get; set; }
        public string QualificationLevel { get; set; }
        public string WorkType { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime Deadline { get; set; }
        public int CandidateId { get; set; }
        public string CandidateFirstName { get; set; }
        public string CandidateLastName { get; set; }
        public string CandidateFullName { get; set; }
        public DateTime ApplicationDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
