using Core.Entites.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserOperationClaimDal
    {
        void Add(UserOperationClaim userOperationClam);
        void Delete(UserOperationClaim userOperationClam);
        void Update(UserOperationClaim userOperationClaim);
        UserOperationClaim GetById(int id);
        List<UserOperationClaim> GetAll();
        List<UserOperationClaim> GetByUserId(int userId);
    }
}
