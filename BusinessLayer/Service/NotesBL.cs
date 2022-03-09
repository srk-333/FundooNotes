//-----------------------------------------------------------------------
// <copyright file="NotesBL.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using RepoLayer.Entity;
    using RepoLayer.Interface;

    /// <summary>
    /// Service Class notes
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.INotesBL" />
    public class NotesBL : INotesBL
    {
        /// <summary>
        /// The notes interface
        /// </summary>
        private readonly INotesRL notesRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesBL"/> class.
        /// </summary>
        /// <param name="notesRL">The notes interface.</param>
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// note details
        /// </returns>
        public Notes CreateNote(NotesModel notesModel, long userId)
        {
            try
            {
                return this.notesRL.CreateNote(notesModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// updated note details
        /// </returns>
        public Notes UpdateNote(UpdateNote notesModel, long noteId)
        {
            try
            {
                return this.notesRL.UpdateNote(notesModel, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// true or false
        /// </returns>
        public bool DeleteNote(long noteId)
        {
            try
            {
                return this.notesRL.DeleteNote(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// note details by user id
        /// </returns>
        public IEnumerable<Notes> GetNote(long userId)
        {
            try
            {
                return this.notesRL.GetNote(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>
        /// all note details from table
        /// </returns>
        public IEnumerable<Notes> GetAllNotes()
        {
            try
            {
                return this.notesRL.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Determines whether [is archive or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// note details
        /// </returns>
        public Notes IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                return this.notesRL.IsArchieveOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Determines whether [is trash or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// note details
        /// </returns>
        public Notes IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                return this.notesRL.IsTrashOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Determines whether [is pin or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// note details
        /// </returns>
        public Notes IsPinOrNot(long noteId, long userId)
        {
            try
            {
                return this.notesRL.IsPinOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Does the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        /// note details
        /// </returns>
        public Notes DoColour(long noteId, string color)
        {
            try
            {
                return this.notesRL.DoColour(noteId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>
        /// note details
        /// </returns>
        public Notes UploadImage(long noteId, long userId, IFormFile image)
        {
            try
            {
                return this.notesRL.UploadImage(noteId, userId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Makes the copy of note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// copy of note
        /// </returns>
        public Notes MakeCopyOfNote(long noteId, long userId)
        {
            try
            {
                return this.notesRL.MakeCopyOfNote(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
