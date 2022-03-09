//-----------------------------------------------------------------------
// <copyright file="Collabarator.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepoLayer.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// Collaborator Class For Creating Table in Database 
    /// </summary>
    public class Collabarator
    {
        /// <summary>
        /// Gets or sets the Collaborator identifier.
        /// </summary>
        /// <value>
        /// The Collaborator identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }

        /// <summary>
        /// Gets or sets the Collaborator email.
        /// </summary>
        /// <value>
        /// The Collaborator email.
        /// </value>
        public string CollabEmail { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [ForeignKey("user")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        [ForeignKey("note")]
        public long NotesId { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public Notes Notes { get; set; }
    }
}