//-----------------------------------------------------------------------
// <copyright file="LabelBL.cs" company="Saurav">
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
    /// Service Class for label
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.ILabelBL" />
    public class LabelBL : ILabelBL
    {
        /// <summary>
        /// The label interface
        /// </summary>
        private readonly ILabelRL labelRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelBL"/> class.
        /// </summary>
        /// <param name="labelRL">The label interface.</param>
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        /// <summary>
        /// Adds the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// label details
        /// </returns>
        public Labels AddLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                return this.labelRL.AddLabelName(labelName, noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// updated label details
        /// </returns>
        public Labels UpdateLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                return this.labelRL.UpdateLabelName(labelName, noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// true or false
        /// </returns>
        public bool RemoveLabel(long labelId, long userId)
        {
            try
            {
                return this.labelRL.RemoveLabel(labelId, userId);
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
        /// labels by note id
        /// </returns>
        public IEnumerable<Labels> GetByNoteId(long noteId)
        {
            try
            {
                return this.labelRL.GetByNoteId(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// labels by user id
        /// </returns>
        public IEnumerable<Labels> GetByUserId(long userId)
        {
            try
            {
                return this.labelRL.GetByUserId(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>
        /// all labels
        /// </returns>
        public IEnumerable<Labels> GetAllLabels()
        {
            try
            {
                return this.labelRL.GetAllLabels();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
