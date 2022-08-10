using Core.DataAccess;
using Core.Entites.Concrete;
using Entities.Dto;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        List<UserDto> GetUserDetails();
        List<UserDto> GetAllDetails();
    }
}
