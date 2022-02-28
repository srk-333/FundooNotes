using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Service
{
    //Service Class
    public class UserRL : IUserRL
    {
        //instance of  FundooContext Class
        private readonly FundooContext fundooContext;
        private readonly IConfiguration _Appsettings;
        //Constructor
        public UserRL(FundooContext fundooContext, IConfiguration _Appsettings)
        {
            this.fundooContext = fundooContext;
            this._Appsettings = _Appsettings;
        }
        //Method to register User Details.
        public User Registration(UserRegistration userRegist)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = userRegist.FirstName;
                newUser.LastName = userRegist.LastName;
                newUser.Email = userRegist.Email;
                newUser.Password = userRegist.Password;
                fundooContext.UserTable.Add(newUser);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                    return newUser;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Validating Email And Password
        public string Login(string email, string password)
        {
            try
            {
                //if Email and password is empty return null. 
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return null;
                //Linq query matches given input in database and returns that entry from the database.
                var result = fundooContext.UserTable.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                var id = result.Id;
                if (result != null)
                    //Calling Jwt Token Creation method and returning token.
                    return GenerateSecurityToken(result.Email, id);
                else
                    return null;            
            }
            catch (Exception)
            {
                throw;
            }
        }    
        //Implementing Jwt Token For Login using Email and Id
        private string GenerateSecurityToken(string Email, long Id)
        {
            //header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Appsettings["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //payload
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("Id",Id.ToString())
            };
            //signature
            var token = new JwtSecurityToken(_Appsettings["Jwt:Issuer"],
              _Appsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //Forgot password Token Generation
        public string ForgotPassword(string email)
        {
            try
            {
                //check Email Exists or Not.
                var existingEmail = this.fundooContext.UserTable.Where(E => E.Email == email).FirstOrDefault();
                if (existingEmail != null)
                {
                    //Generating Token 
                    var token = GenerateSecurityToken(existingEmail.Email, existingEmail.Id);
                    //passing Token to MsmqModel.
                    new MsmqModel().Sender(token);
                    return token;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
