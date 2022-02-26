using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    //Service Class
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        //Constructor
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        //Method to register User Details.
        public User Registration(UserRegistration userRegist)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = userRegist.FirstName;
                newUser.LastName = userRegist.LastName;
                newUser.Email = userRegist.Email;
                newUser.Password = userRegist.Password;
                fundooContext.UserTable.Add(newUser);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                    return newUser;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
