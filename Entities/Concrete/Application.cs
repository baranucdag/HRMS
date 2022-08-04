using Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class Application : IEntity
    {
        public int Id { get; set; }
        public int JobAdvertId { get; set; }
        public int CandidateId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int IsDeleted { get; set; }
    }
}
