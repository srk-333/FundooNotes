//-----------------------------------------------------------------------
// <copyright file="ILabelRL.cs" company="Saurav">
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
    public interface ILabelRL
    {
        /// <summary>
        /// Adds the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>label detail </returns>
        public Labels AddLabelName(string labelName, long noteId, long userId);

        /// <summary>
        /// Updates the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> updated label details </returns>
        public Labels UpdateLabelName(string labelName, long noteId, long userId);

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> true or false </returns>
        public bool RemoveLabel(long labelId, long userId);

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> label by note id </returns>
        public IEnumerable<Labels> GetByNoteId(long noteId);

        /// <summary>
        /// Gets the by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> label by user id </returns>
        public IEnumerable<Labels> GetByUserId(long userId);

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns> All labels from table </returns>
        public IEnumerable<Labels> GetAllLabels();
    }
}