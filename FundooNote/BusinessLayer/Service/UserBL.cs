﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;

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
    }
}