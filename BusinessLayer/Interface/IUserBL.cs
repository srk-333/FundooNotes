﻿using CommonLayer.Models;
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
    }
}
