//-----------------------------------------------------------------------
// <copyright file="ICollabBL.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using RepoLayer.Entity;

    /// <summary>
    /// Business Layer collaborator interface
    /// </summary>
    public interface ICollabBL
    {
        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> collaborator detail </returns>
        public Collabarator AddCollab(string email, long userId, long noteId);

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The collaborator identifier.</param>
        /// <returns> removed collaborator </returns>
        public Collabarator RemoveCollab(long userId, long collabId);

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> collaborator by note id </returns>
        public IEnumerable<Collabarator> GetByNoteId(long noteId);

        /// <summary>
        /// Gets all collaborator.
        /// </summary>
        /// <returns>all collaborator</returns>
        public IEnumerable<Collabarator> GetAllCollab();
    }
}
