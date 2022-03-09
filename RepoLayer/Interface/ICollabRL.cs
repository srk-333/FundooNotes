//-----------------------------------------------------------------------
// <copyright file="ICollabRL.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepoLayer.Interface
{ 
    using System;
    using System.Collections.Generic;
    using System.Text;   
    using RepoLayer.Entity;

    /// <summary>
    ///  interface class
    /// </summary>
    public interface ICollabRL
    {
        /// <summary>
        /// Adds the Collaborator.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Collaborator Details</returns>
        public Collabarator AddCollab(string email, long userId, long noteId);

        /// <summary>
        /// Removes the Collaborator.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The Collaborator identifier.</param>
        /// <returns> removed Collaborator detail </returns>
        public Collabarator RemoveCollab(long userId, long collabId);

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> Collaborator Detail by note id </returns>
        public IEnumerable<Collabarator> GetByNoteId(long noteId);

        /// <summary>
        /// Gets all Collaborator.
        /// </summary>
        /// <returns> All Collaborator details from table</returns>
        public IEnumerable<Collabarator> GetAllCollab();
    }
}