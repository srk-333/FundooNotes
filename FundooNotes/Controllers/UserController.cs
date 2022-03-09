//-----------------------------------------------------------------------
// <copyright file="UserController.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// controller for user class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        /// <summary>
        /// The user interface from business layer
        /// </summary>
        private readonly IUserBL userBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userBL">The user interface.</param>
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        /// <summary>
        /// Registers the specified user registration.
        /// </summary>
        /// <param name="userRegistration">The user registration.</param>
        /// <returns>user details</returns>
        [HttpPost("Register")]
        public IActionResult Register(UserRegistration userRegistration)
        {
            try
            {
                var result = this.userBL.Registration(userRegistration);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Registration successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Logins the specified user login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns> token for logged user </returns>
        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var user = this.userBL.Login(userLogin.Email, userLogin.Password);
                if (user != null)
                {
                    return this.Ok(new { Success = true, message = "Logged In", data = user });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email and Password" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Forgot the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns> Token on mail </returns>
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var token = this.userBL.ForgotPassword(email);
                if (token != null)
                {
                    return this.Ok(new { Success = true, message = "Email Sent to Your mail", data = token });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Error while sending Mail ! try Again" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns> true or false </returns>
        [Authorize]
        [HttpPut("RestPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                // Take Email For which password had to be Changed.
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                if (this.userBL.ResetPassword(email, password, confirmPassword))
                {
                    return this.Ok(new { Success = true, message = "Password Changed Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Password doesn't Match ! Please try again" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
