using Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class CandidateTrainingOrCourse : BaseEntity, IEntity
    {
        public int CandidateId { get; set; }
        public DateTime Year { get; set; }
        public string TrainOrCourseDetails { get; set; }
        public string Issuer { get; set; }
        public string ValidityDate { get; set; }
    }
}
