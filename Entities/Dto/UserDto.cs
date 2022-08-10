﻿using Entities.Abstract;

namespace Entities.Dto
{
    public class UserDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string UserClaim { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
