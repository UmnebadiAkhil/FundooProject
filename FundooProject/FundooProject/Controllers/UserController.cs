using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        //Constructor
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        //Register a User
        [HttpPost("Register")]
        public IActionResult Register(UserRegistration userRegistration)
        {
            try
            {
                var result = userBL.Registration(userRegistration);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Registration successful", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessful" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var user = userBL.Login(userLogin.Email, userLogin.Password);
                if (user != null)
                    return this.Ok(new { Success = true, message = "Logged In", data = user });
                else
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email and Password" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var user = userBL.ForgetPassword(email);
                if (user != null)
                    return this.Ok(new { Success = true, message = "Email sent", data = user });
                else
                    return this.BadRequest(new { Success = false, message = "Email not sent" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPost("ResetPassword")]
   
        public IActionResult ResetPassword( string password, string newPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetPassword(email, password, newPassword);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Password updated sucessfully"});
                else
                    return this.BadRequest(new { Success = false, message = "Unable to reset password. Please try again!" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
