﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public User Registration(UserRegistration userRegist);
        public string Login(string email, string password);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string password, string newPassword);
        public bool DeleteUser(string email);
    }
}
