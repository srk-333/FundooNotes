//-----------------------------------------------------------------------
// <copyright file="UserBL.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using RepoLayer.Entity;
    using RepoLayer.Interface;

    /// <summary>
    /// Service Class of Business Layer
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.IUserBL" />
    public class UserBL : IUserBL
    {      
        /// <summary>
        /// instance of RepoLayer Interface
        /// </summary>
        private readonly IUserRL userRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBL"/> class.
        /// </summary>
        /// <param name="userRL">The user interface.</param>
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        /// <summary>
        /// Registrations the specified user register.
        /// </summary>
        /// <param name="userRegist">The user register model.</param>
        /// <returns>
        /// user details which added in database
        /// </returns>
        public User Registration(UserRegistration userRegist)
        {
            try
            {
                return this.userRL.Registration(userRegist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// token with login
        /// </returns>
        public string Login(string email, string password)
        {
            try
            {
                return this.userRL.Login(email, password);
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
        /// <returns>
        /// token for forgot password
        /// </returns>
        public string ForgotPassword(string email)
        {
            try
            {
                return this.userRL.ForgotPassword(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>
        /// true or false
        /// </returns>
        public bool ResetPassword(string email, string password, string newPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email, password, newPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the account.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// true or false
        /// </returns>
        public bool DeleteAccount(string email)
        {
            try
            {
                return this.userRL.DeleteAccount(email);
            }
            catch (Exception)
            {
                throw;
            }
        } 
    }
}
