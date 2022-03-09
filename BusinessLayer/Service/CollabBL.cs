//-----------------------------------------------------------------------
// <copyright file="CollabBL.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using RepoLayer.Entity;
    using RepoLayer.Interface;

    /// <summary>
    /// Service Class
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.ICollabBL" />
    public class CollabBL : ICollabBL
    {
        /// <summary>
        /// The collaborator Interface
        /// </summary>
        private readonly ICollabRL collabRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabBL"/> class.
        /// </summary>
        /// <param name="collabRL">The collaborator interface.</param>
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// collaborator detail
        /// </returns>
        public Collabarator AddCollab(string email, long userId, long noteId)
        {
            try
            {
                return this.collabRL.AddCollab(email, userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The collaborator identifier.</param>
        /// <returns>
        /// removed collaborator detail
        /// </returns>
        public Collabarator RemoveCollab(long userId, long collabId)
        {
            try
            {
                return this.collabRL.RemoveCollab(userId, collabId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// collaborator by note id
        /// </returns>
        public IEnumerable<Collabarator> GetByNoteId(long noteId)
        {
            try
            {
                return this.collabRL.GetByNoteId(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all collaborator.
        /// </summary>
        /// <returns>
        /// all collaborator
        /// </returns>
        public IEnumerable<Collabarator> GetAllCollab()
        {
            try
            {
                return this.collabRL.GetAllCollab();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
