using Business.Abstract;
using Core.Entites.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal userDal;
        public UserService(IUserDal userDal)
        {
            this.userDal = userDal;
        }
        public void Add(User user)
        {
            userDal.Add(user);
        }

        public void Delete(int id)
        {
            var deletedEntity = userDal.Get(x=>x.Id == id);
            userDal.SoftDelete(deletedEntity);
        }

        public List<User> GetAll()
        {
            return userDal.GetAll();    
        }

        public User GetById(int id)
        {
            return userDal.Get(x => x.Id == id);
        }

        public User GetUserEMail(string email)
        {
            return userDal.Get(x=>x.EMail == email);
        }

        public void Update(User user)
        {
            userDal.Update(user);
        }
    }
}
