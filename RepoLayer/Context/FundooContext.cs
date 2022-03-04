using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Context
{
    //Context Class inheriting DbContext
    //DbContext is used to communicate with Database
    public class FundooContext : DbContext
    {   
        //Constructor
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }
        //DbSet used to view Database and interact with Table in Database.
        public DbSet<User> UserTable { get; set; }
        public DbSet<Notes> NotesTable { get; set; }
        public DbSet<Collabarator> CollabTable { get; set; }
    }
}
