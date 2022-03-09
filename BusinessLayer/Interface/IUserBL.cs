//-----------------------------------------------------------------------
// <copyright file="IUserBL.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;
    using RepoLayer.Entity;

    /// <summary>
    /// Business Layer Notes interface
    /// </summary>
    public interface IUserBL
    {
        /// <summary>
        /// Registrations the specified user register.
        /// </summary>
        /// <param name="userRegist">The user register model.</param>
        /// <returns>user details which added in database</returns>
        public User Registration(UserRegistration userRegist);

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns> token with login </returns>
        public string Login(string email, string password);

        /// <summary>
        /// Forgot the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns> token for forgot password </returns>
        public string ForgotPassword(string email);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns> true or false </returns>
        public bool ResetPassword(string email, string password, string newPassword);
    }
}
