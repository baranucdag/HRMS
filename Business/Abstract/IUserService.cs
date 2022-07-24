﻿using Core.Entites.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        List<User> GetAll();
        User GetById(int id);
        User GetUserEMail(string email);
    }
}
