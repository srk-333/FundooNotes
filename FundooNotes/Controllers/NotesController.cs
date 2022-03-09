//-----------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNotes.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using RepoLayer.Entity; 

    /// <summary>
    /// controller class for notes
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The notes interface from business layer
        /// </summary>
        private readonly INotesBL notesBL;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesBL">The notes interface.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns> Notes Details </returns>
        [Authorize]
        [HttpPost("Create")]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.CreateNote(notesModel, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Notes created  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> updated note details </returns>
        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateNotes(UpdateNote notesModel, long noteId)
        {
            try
            {            
                var notes = this.notesBL.UpdateNote(notesModel, noteId);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = " Notes Updated  successfully ", data = notes });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed! Didn't updated" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>true or false </returns>
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long noteId)
        {
            try
            {
                if (this.notesBL.DeleteNote(noteId))
                {
                    return this.Ok(new { Success = true, message = " Notes Deleted  successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed ! Try again" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the notes by user identifier.
        /// </summary>
        /// <returns> notes by user id </returns>
        [Authorize]
        [HttpGet("{Id}/Get")]
        public IEnumerable<Notes> GetNotesByUserId()
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.GetNote(userId);
                if (result != null)
                {
                    return result;
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
        /// Gets all notes.
        /// </summary>
        /// <returns>All notes details </returns>
        [HttpGet("GetAll")]
        public IEnumerable<Notes> GetAllNotes()
        {
            try
            {
                var result = this.notesBL.GetAllNotes();
                if (result != null)
                {
                    return result;
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
        /// Gets all notes using cache.
        /// </summary>
        /// <returns> all notes from cache </returns>
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "notesList";
            string serializedNotesList;
            var notesList = new List<Notes>();
            var redisNotesList = await this.distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                notesList = JsonConvert.DeserializeObject<List<Notes>>(serializedNotesList);
            }
            else
            {
                notesList = (List<Notes>)this.notesBL.GetAllNotes();
                serializedNotesList = JsonConvert.SerializeObject(notesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(15))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await this.distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }

            return this.Ok(notesList);
        }

        /// <summary>
        /// Determines whether [is archive or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>note details</returns>
        [Authorize]
        [HttpPost("IsArchieve")]
        public IActionResult IsArchieveOrNot(long noteId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.IsArchieveOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "  Is Trash Or Not checked ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessful" });
                }
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
        /// <returns> note details </returns>
        [Authorize]
        [HttpPut("IsTrash")]
        public IActionResult IsTrashOrNot(long noteId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.IsTrashOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Is Trash Or Not checked ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessful" });
                }
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
        /// <returns> note details </returns>
        [Authorize]
        [HttpPut("IsPin")]
        public IActionResult IsPinOrNot(long noteId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.IsPinOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Is Pin Or Not checked ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Unsuccessful " });
                }
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
        /// <returns> note details </returns>
        [Authorize]
        [HttpPut("Colour")]
        public IActionResult DoColour(long noteId, string color)
        {
            try
            {
                var result = this.notesBL.DoColour(noteId, color);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Notes Coloured  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Colour Unsuccessful" });
                }
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
        /// <param name="image">The image.</param>
        /// <returns>note details with uploaded image </returns>
        [Authorize]
        [HttpPost("Image")]
        public IActionResult UploadImage(long noteId, IFormFile image)
        {
            try
            {
                // Take id of  Logged In User
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.UploadImage(noteId, userId, image);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Image Uploaded Successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Image Upload Failed ! Try Again " });
                }
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
        /// <returns> copy of existing note </returns>
        [Authorize]
        [HttpPost("MakeCopy")]
        public IActionResult MakeCopyOfNote(long noteId)
        {
            try
            {
                // Take id of  Logged In User
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.MakeCopyOfNote(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Created a Copy Of Note Successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed to create a Copy of  Note ! Try Again " });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
