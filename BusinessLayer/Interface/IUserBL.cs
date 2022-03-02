using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    //Business Layer User interface
    public  interface IUserBL
    {
        public User Registration(UserRegistration userRegist);
        public string Login(string email, string password);
        public string ForgotPassword(string email);
        public bool ResetPassword(string email, string password, string newPassword);
        public bool DeleteAccount(string email);  
    }
}
