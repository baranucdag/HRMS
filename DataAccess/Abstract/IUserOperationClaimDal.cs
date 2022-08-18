using Core.DataAccess;
using Core.Entites.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IUserOperationClaimDal : IEntityRepository<UserOperationClaim>
    {
        List<UserOperationClaimDto> GetDetails(Expression<Func<UserOperationClaimDto, bool>> filter = null);
        UserOperationClaimDto GetOperationClaimDetail(Expression<Func<UserOperationClaimDto, bool>> filter = null);
    }
}
