using Entities.Abstract;

namespace Entities.Dto
{
    public class UserOperationClaimDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int OperationClaimId { get; set; }
        public string ClaimName { get; set; }
    }
}
