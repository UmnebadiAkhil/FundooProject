using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    //Service Class of Business Layer
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        //Constructor
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        //Method to return UserRegistration obj to Repo Layer User.
        public User Registration(UserRegistration userRegist)
        {
            try
            {
                return userRL.Registration(userRegist);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //User Login
        public string Login(string email, string password)
        {
            try
            {
                return userRL.Login(email, password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // User ForgotPasssword
        public string ForgetPassword(string email)
        {
            try
            {
                return userRL.ForgetPassword(email);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // User ResetPasssword
        public bool ResetPassword(string email, string password, string newPassword)
        {
            try
            {
                return userRL.ResetPassword(email, password, newPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteUser(string email)
        {
            try
            {

                return userRL.DeleteUser(email);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
