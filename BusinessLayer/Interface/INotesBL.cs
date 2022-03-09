//-----------------------------------------------------------------------
// <copyright file="INotesBL.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using RepoLayer.Entity;

    /// <summary>
    /// Business Layer Notes interface
    /// </summary>
    public interface INotesBL
    {
        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> note details </returns>
        public Notes CreateNote(NotesModel notesModel, long userId);

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> updated note details</returns>
        public Notes UpdateNote(UpdateNote notesModel, long noteId);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>true or false </returns>
        public bool DeleteNote(long noteId);

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>note details by user id </returns>
        public IEnumerable<Notes> GetNote(long userId);

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>all note details from table</returns>
        public IEnumerable<Notes> GetAllNotes();

        /// <summary>
        /// Determines whether [is archive or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>note details</returns>
        public Notes IsArchieveOrNot(long noteId, long userId);

        /// <summary>
        /// Determines whether [is trash or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>note details</returns>
        public Notes IsTrashOrNot(long noteId, long userId);

        /// <summary>
        /// Determines whether [is pin or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>note details</returns>
        public Notes IsPinOrNot(long noteId, long userId);

        /// <summary>
        /// Does the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>note details</returns>
        public Notes DoColour(long noteId, string color);

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>note details</returns>
        public Notes UploadImage(long noteId, long userId, IFormFile image);

        /// <summary>
        /// Makes the copy of note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>copy of note</returns>
        public Notes MakeCopyOfNote(long noteId, long userId);
    }
}
