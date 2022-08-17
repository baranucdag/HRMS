namespace Entities.Dto
{
    public class UserUpdateWithClaimDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int OperationClamId { get; set; }
    }
}
