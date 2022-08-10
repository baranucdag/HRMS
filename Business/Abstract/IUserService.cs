using Core.Entites.Concrete;
using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using Entities.Dto;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        List<User> GetAll();
        ResultItem GetById(int id);
        User GetByMail(string email);
        ResultItem GetUsersPaginated(PaginationItem<UserDto> pi);
        List<OperationClaim> GetClaims(User user);
        ResultItem GetClaim(User user);
    }
}
