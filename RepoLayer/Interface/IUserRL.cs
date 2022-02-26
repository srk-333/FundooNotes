using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    //Repo Layer User interface
    public interface IUserRL
    {
        public User Registration(UserRegistration userRegist);
        public string Login(string email, string password);
    }
}
