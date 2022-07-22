using Core.DataAccess.EntityFramework;
using Core.Entites.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, DataContext>, IUserOperationClaimDal
    {
    }
}
