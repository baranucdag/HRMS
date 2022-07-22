using Core.DataAccess.EntityFramework;
using Core.Entites.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, DataContext>, IUserDal
    {
    }
}
