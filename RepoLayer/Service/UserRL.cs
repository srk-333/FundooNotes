//-----------------------------------------------------------------------
// <copyright file="UserRL.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepoLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using CommonLayer.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using RepoLayer.Context;
    using RepoLayer.Entity;
    using RepoLayer.Interface;

    /// <summary>
    /// Service Class
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.IUserRL" />
    public class UserRL : IUserRL
    {    
        /// <summary>
        /// instance of Classes
        /// </summary>
        private readonly FundooContext fundooContext;

        /// <summary>
        /// interface of configuration class
        /// </summary>
        private readonly IConfiguration appsettings;

        /// <summary> Initializes a new instance of the <see cref="UserRL" /> class. </summary>
        /// <param name="fundooContext"> The fundo Context </param>
        /// <param name="appsettings"> The App settings </param>
        public UserRL(FundooContext fundooContext, IConfiguration appsettings)
        {
            this.fundooContext = fundooContext;
            this.appsettings = appsettings;
        }

        /// <summary>
        /// Method to register User Details.
        /// </summary>
        /// <param name="userRegist"> Takes Model class</param>
        /// <returns> user Details </returns>
        public User Registration(UserRegistration userRegist)
        {
            try
            {
                User newUser = new User
                {
                    FirstName = userRegist.FirstName,
                    LastName = userRegist.LastName,
                    Email = userRegist.Email,
                    Password = this.EncryptPass(userRegist.Password)
                };

                // Adding User Details in the Database.
                this.fundooContext.UserTable.Add(newUser);

                // Save Changes Made in database
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return newUser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Encrypt Password
        /// </summary>
        /// <param name="password"> Takes password </param>
        /// <returns> Encrypted password </returns>
        public string EncryptPass(string password)
        {
            try
            {
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                string encryptPass = Convert.ToBase64String(encode);
                return encryptPass;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Decrypt Password
        /// </summary>
        /// <param name="encodedPass"> Takes encrypted password</param>
        /// <returns> decrypted password </returns>
        public string DecryptPass(string encodedPass)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();
                byte[] toDecodeByte = Convert.FromBase64String(encodedPass);
                int charCount = utf8Decode.GetCharCount(toDecodeByte, 0, toDecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(toDecodeByte, 0, toDecodeByte.Length, decodedChar, 0);
                string decryptPass = new string(decodedChar);
                return decryptPass;
            }
            catch (Exception)
            {
                throw;
            }
        }     

        /// <summary>
        /// Validating Email And Password
        /// </summary>
        /// <param name="email"> Takes email </param>
        /// <param name="password"> Takes password </param>
        /// <returns> Token for logged in user </returns>
        public string Login(string email, string password)
        {
            try
            {
                // if Email and password is empty return null. 
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return null;
                }

                // Linq query matches given input in database and returns that user details from the database.
                var result = this.fundooContext.UserTable.FirstOrDefault(x => x.Email == email);
                string dcryptPass = this.DecryptPass(result.Password);
                var id = result.Id;
                if (result != null && dcryptPass == password)
                {
                    // Calling Jwt Token Creation method and returning token.
                    return this.GenerateSecurityToken(result.Email, id);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Forgot password Token Generation
        /// </summary>
        /// <param name="email">Takes email </param>
        /// <returns> Token for forgot password </returns>
        public string ForgotPassword(string email)
        {
            try
            {
                // Fetching user details from database With the email Id, if present in database.
                var existingEmail = this.fundooContext.UserTable.Where(E => E.Email == email).FirstOrDefault();
                if (existingEmail != null)
                {
                    // Generating Token 
                    var token = this.GenerateSecurityToken(existingEmail.Email, existingEmail.Id);

                    // passing Token to MsmqModel.
                    new MsmqModel().Sender(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method will update new Password in the database
        /// </summary>
        /// <param name="email"> Takes email </param>
        /// <param name="password"> Takes password </param>
        /// <param name="newPassword">Takes confirm password </param>
        /// <returns> True or False </returns>
        public bool ResetPassword(string email, string password, string newPassword)
        {
            try
            {
                // Checks both password matching or Not
                if (password.Equals(newPassword))
                {
                    // Fetching user details from database With the email Id , if present in database.
                    var user = this.fundooContext.UserTable.Where(e => e.Email == email).FirstOrDefault();

                    // Updating Password
                    user.Password = this.EncryptPass(newPassword);

                    // Saving changes made in database.
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Delete a User from database.
        /// </summary>
        /// <param name="email">Takes mail </param>
        /// <returns>True or False </returns>
        public bool DeleteAccount(string email)
        {
            try
            {
                // Fetching user details from database With the email Id , if present in database.
                var user = this.fundooContext.UserTable.Where(e => e.Email == email).FirstOrDefault();
                if (user != null)
                {
                    // Remove the user from database.
                    this.fundooContext.UserTable.Remove(user);

                    // Saving changes made in database.
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Implementing Token For Login using Email and Id
        /// </summary>
        /// <param name="email"> Takes email </param>
        /// <param name="id"> Takes user id </param>
        /// <returns> Token for user </returns>
        private string GenerateSecurityToken(string email, long id)
        {
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appsettings["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // payload
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim("Id", id.ToString()),
            };

            // signature
            var token = new JwtSecurityToken(
                this.appsettings["Jwt:Issuer"],
                this.appsettings["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
