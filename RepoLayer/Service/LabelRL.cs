//-----------------------------------------------------------------------
// <copyright file="LabelRL.cs" company="Saurav">
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
    /// <seealso cref="RepoLayer.Interface.ILabelRL" />
    public class LabelRL : ILabelRL
    {
        /// <summary>
        /// The fundo context
        /// </summary>
        private readonly FundooContext fundooContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRL"/> class.
        /// </summary>
        /// <param name="fundooContext">The fundo context.</param>
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        /// <summary>
        /// Adds the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> Label detail </returns>
        public Labels AddLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                Labels label = new Labels
                {
                    LabelName = labelName,
                    Id = userId,
                    NotesId = noteId
                };

                // Add all the details in Label Table.
                this.fundooContext.LabelTable.Add(label);

                // Save Changes Made in the database
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return label;
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
        /// Updates the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Label detail </returns>
        public Labels UpdateLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                // Fetch All the details with the given noteId and userId.
                var label = this.fundooContext.LabelTable.FirstOrDefault(l => l.NotesId == noteId && l.Id == userId);
                if (label != null)
                {
                    label.LabelName = labelName;

                    // Update database for given LabelName.
                    this.fundooContext.LabelTable.Update(label);

                    // Save Changes Made in the database
                    this.fundooContext.SaveChanges();
                    return label;
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
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> True or False </returns>
        public bool RemoveLabel(long labelId, long userId)
        {
            try
            {
                // Fetch All the details with the given labelId.
                var labelDetails = this.fundooContext.LabelTable.FirstOrDefault(l => l.LabelId == labelId && l.Id == userId);
                if (labelDetails != null)
                {
                    // Remove Label details from database
                    this.fundooContext.LabelTable.Remove(labelDetails);

                    // Save Changes Made in the database
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
        /// <returns> Label detail by note id </returns>
        public IEnumerable<Labels> GetByNoteId(long noteId)
        {
            try
            {
                // Fetch All the details with the given noteid.
                var data = this.fundooContext.LabelTable.Where(d => d.NotesId == noteId).ToList();
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
        /// Gets the by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Label detail by user id</returns>
        public IEnumerable<Labels> GetByUserId(long userId)
        {
            try
            {
                // Fetch All the details with the given userid.
                var data = this.fundooContext.LabelTable.Where(d => d.Id == userId).ToList();
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
        /// Gets all labels.
        /// </summary>
        /// <returns> All label from table </returns>
        public IEnumerable<Labels> GetAllLabels()
        {
            try
            {
                // Fetch All the details from Labels Table
                var labels = this.fundooContext.LabelTable.ToList();
                if (labels != null)
                {
                    return labels;
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
