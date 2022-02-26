using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    //Repo Layer User interface
    public interface IUserRL
    {
        public User Registration(UserRegistration userRegist);
    }
}
