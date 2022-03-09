//-----------------------------------------------------------------------
// <copyright file="IUserRL.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepoLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;
    using RepoLayer.Entity;

    /// <summary>
    ///  interface class
    /// </summary>
    public interface IUserRL
    {
        /// <summary>
        /// Registrations the specified user
        /// </summary>
        /// <param name="userRegist">The user registration model.</param>
        /// <returns> Added user details</returns>
        public User Registration(UserRegistration userRegist);

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns> login token </returns>
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

        /// <summary>
        /// Deletes the account.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns> true or false </returns>
        public bool DeleteAccount(string email); 
    }
}