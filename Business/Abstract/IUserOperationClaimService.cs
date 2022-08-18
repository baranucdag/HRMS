using Core.Entites.Concrete;
using Core.Utilities.Helpers.PaginationHelper;
using Core.Utilities.Result;
using Entities.Dto;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        ResultItem Add(UserOperationClaim userOperationClam);
        ResultItem Delete(UserOperationClaim userOperationClam);
        ResultItem Update(UserOperationClaim userOperationClaim);
        ResultItem GetclaimsPaginated(PaginationItem<UserOperationClaimDto> pi);
        ResultItem GetById(int id);
        ResultItem GetAll();
        ResultItem GetByUserId(int userId);
    }
}
