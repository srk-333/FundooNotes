//-----------------------------------------------------------------------
// <copyright file="UserLogin.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    ///  User login model class
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }
}
