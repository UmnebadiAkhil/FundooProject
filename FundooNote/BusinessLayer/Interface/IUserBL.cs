using CommonLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
    //Business Layer User interface
    public interface IUserBL
    {
        public User Registration(UserRegistration userRegist);
    }
}