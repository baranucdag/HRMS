using Core.DataAccess;
using Core.Entites.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
