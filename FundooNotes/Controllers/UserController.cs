using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase     //UserController having all Apis for User
    {
        //instance of BusinessLayer Interface
        private readonly IUserBL userBL;
        //Constructor
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        //User Register Api
        [HttpPost("Register")]
        public IActionResult Register(UserRegistration userRegistration)
        {
            try
            {
                var result = userBL.Registration(userRegistration);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Registration successful", data = result});
                else
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessful" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //User Login Api
        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var user = userBL.Login(userLogin.Email, userLogin.Password);
                if (user != null)
                    return this.Ok(new { Success = true, message = "Logged In", data = user});
                else
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email and Password" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //User Forgot Password Api
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var token = userBL.ForgotPassword(email);
                if (token != null)
                    return this.Ok(new { Success = true, message = "Email Sent to Your mail", data = token });
                else
                    return this.BadRequest(new { Success = false, message = "Error while sending Mail ! try Again" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //User Reset Password Api
        [Authorize]
        [HttpPut("RestPassword")]
        public IActionResult ResetPassword(string password , string confirmPassword)
        {
            try
            {
                //Take Email For which password had to be Changed.
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                if (userBL.ResetPassword(email, password, confirmPassword))
                    return this.Ok(new { Success = true, message = "Password Changed Successfully"});
                else
                    return this.BadRequest(new { Success = false, message = "Password doesn't Match ! Please try again" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Delete Account Api
        [HttpDelete("DeleteAccount")]
        public IActionResult DeleteAccount(string email)
        {
            try
            {
                if (userBL.DeleteAccount(email))
                    return this.Ok(new { Success = true, message = "Account Deleted Successfully" });
                else
                    return this.BadRequest(new { Success = false, message = "Unable to Delete your Account ! Please try again" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
