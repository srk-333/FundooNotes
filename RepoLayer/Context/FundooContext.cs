//-----------------------------------------------------------------------
// <copyright file="fundoocontext.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepoLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using RepoLayer.Entity;

    /// <summary>
    /// fundo context class used to create database and Tables.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class FundooContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FundooContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user table.
        /// </summary>
        /// <value>
        /// The user table.
        /// </value>
        public DbSet<User> UserTable { get; set; }

        /// <summary>
        /// Gets or sets the notes table.
        /// </summary>
        /// <value>
        /// The notes table.
        /// </value>
        public DbSet<Notes> NotesTable { get; set; }

        /// <summary>
        /// Gets or sets the Collaborator table.
        /// </summary>
        /// <value>
        /// The Collaborator table.
        /// </value>
        public DbSet<Collabarator> CollabTable { get; set; }

        /// <summary>
        /// Gets or sets the label table.
        /// </summary>
        /// <value>
        /// The label table.
        /// </value>
        public DbSet<Labels> LabelTable { get; set; }
    }
}