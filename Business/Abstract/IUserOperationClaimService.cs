using Core.Entites.Concrete;
using Core.Utilities.Result;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        ResultItem Add(UserOperationClaim userOperationClam);
        ResultItem Delete(UserOperationClaim userOperationClam);
        ResultItem Update(UserOperationClaim userOperationClaim);
        ResultItem GetById(int id);
        ResultItem GetAll();
        ResultItem GetByUserId(int userId);
    }
}
