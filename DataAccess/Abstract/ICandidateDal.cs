using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICandidateDal : IEntityRepository<Candidate>
    {
    }
}
