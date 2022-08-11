using Core.DataAccess.EntityFramework;
using Core.Entites.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Dto;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, DataContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new DataContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public List<UserDto> GetAllDetails()
        {
            using (var context = new DataContext())
            {
                var result = from t1 in context.Users
                             join t2 in context.UserOperationClaims
                             on t1.Id equals t2.UserId

                             join t3 in context.OperationClaims
                             on t2.OperationClaimId equals t3.Id

                             select new UserDto
                             {
                                 Id = t1.Id,
                                 Email = t1.Email,
                                 FirstName = t1.FirstName,
                                 LastName = t1.LastName,
                                 UserClaim = t3.Name,
                                 Status = t1.Status
                             };
                return result.ToList();
            }
        }


        public List<UserDto> GetUserDetails()
        {
            List<UserDto> result = new List<UserDto>();
            using (var context = new DataContext())
            {
                var query = context.Set<User>().Select(x => new UserDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Status = x.Status
                });  
                result = query.ToList();
            }
            return result;
        }
       
    }
}
