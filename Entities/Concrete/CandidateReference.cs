using Entities.Abstract;

namespace Entities.Concrete
{
    public class CandidateReference : IEntity
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string ReferenceFullName { get; set; }
        public string ReferenceAdress { get; set; }
        public string ReferenceProfession { get; set; }
        public string ReferencePhoneNumber { get; set; }
        public int IsDeleted { get; set; }
    }
}
