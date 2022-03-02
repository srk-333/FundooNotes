using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    //Service Class of Business Layer
    public class UserBL : IUserBL
    {
        //instance of RepoLayer Interface
        private readonly IUserRL userRL;
        //Constructor
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        //User registration
        public User Registration(UserRegistration userRegist)
        {
            try
            {
                return userRL.Registration(userRegist);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //User Login
        public string Login(string email, string password)
        {
            try
            {
                return userRL.Login(email, password);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Forgot Password 
        public string ForgotPassword(string email)
        {
            try
            {
                return userRL.ForgotPassword(email);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Reset Password
        public bool ResetPassword(string email, string password, string newPassword)
        {
            try
            {
                return userRL.ResetPassword(email, password, newPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Delete Account 
        public bool DeleteAccount(string email)
        {
            try
            {
                return userRL.DeleteAccount(email);
            }
            catch (Exception)
            {
                throw;
            }
        } 
    }
}
