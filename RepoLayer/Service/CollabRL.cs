//-----------------------------------------------------------------------
// <copyright file="CollabRL.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepoLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RepoLayer.Context;
    using RepoLayer.Entity;
    using RepoLayer.Interface;
   
    /// <summary>
    ///  Service Class
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.ICollabRL" />
    public class CollabRL : ICollabRL
    {
        /// <summary>
        /// The fundo context
        /// </summary>
        private readonly FundooContext fundooContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabRL"/> class.
        /// </summary>
        /// <param name="fundooContext">The fundo context.</param>
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        /// <summary>
        /// Adds the Collaborator.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> Collaborator Details </returns>
        public Collabarator AddCollab(string email, long userId, long noteId)
        {
            // Fetch All the details from Collab Table by email
            var data = this.fundooContext.UserTable.FirstOrDefault(d => d.Email == email);
            if (data.Email == email)
            {
                Collabarator collab = new Collabarator
                {
                    CollabEmail = email,
                    Id = userId,
                    NotesId = noteId
                };
                this.fundooContext.CollabTable.Add(collab);
                this.fundooContext.SaveChanges();
                return collab;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Removes the Collaborator.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The Collaborator identifier.</param>
        /// <returns> removed Collaborator detail</returns>
        public Collabarator RemoveCollab(long userId, long collabId)
        {
            // Fetch All the details from Collab Table by user id and collab id.
            var data = this.fundooContext.CollabTable.FirstOrDefault(d => d.Id == userId && d.CollabId == collabId);
            if (data != null)
            {
                this.fundooContext.CollabTable.Remove(data);
                this.fundooContext.SaveChanges();
                return data;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> Collaborator detail by note id </returns>
        public IEnumerable<Collabarator> GetByNoteId(long noteId)
        {
            try
            {
                // Fetch All the details from Collab Table by note id.
                var data = this.fundooContext.CollabTable.Where(c => c.NotesId == noteId).ToList();
                if (data != null)
                {
                    return data;
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
        /// Gets all Collaborator.
        /// </summary>
        /// <returns> All Collaborator from table </returns>
        public IEnumerable<Collabarator> GetAllCollab()
        {
            try
            {
                // Fetch All the details from Collab Table
                var collabs = this.fundooContext.CollabTable.ToList();
                if (collabs != null)
                {
                    return collabs;
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
    }
}
