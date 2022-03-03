using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase    //NotesController having all Apis for Notes
    {
        //instance of BusinessLayer Interface
        private readonly INotesBL notesBL;
        //Constructor
        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }
        //Add Notes Api
        [Authorize]
        [HttpPost("AddNote")]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.CreateNote(notesModel , userId);
                if (result != null)
                    return this.Ok(new { Success = true, message = " Notes created  successfully ", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessful" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Updates Notes Api
        [Authorize]
        [HttpPut("UpdateNote")]
        public IActionResult UpdateNotes(UpdateNote notesModel , long noteId)
        {
            try
            {            
                var notes = notesBL.UpdateNote(notesModel, noteId);
                if (notes != null )
                    return this.Ok(new { Success = true, message = " Notes Updated  successfully ", data = notes });
                else
                    return this.BadRequest(new { Success = false, message = "Failed! Didn't updated" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Delete Notes Api
        [Authorize]
        [HttpDelete("DeleteNote")]
        public IActionResult DeleteNotes(long noteId)
        {
            try
            {
                if (notesBL.DeleteNote(noteId))
                    return this.Ok(new { Success = true, message = " Notes Deleted  successfully "});
                else
                    return this.BadRequest(new { Success = false, message = "Failed ! Try again" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Get Notes by UserId Api
        [Authorize]
        [HttpGet("GetNoteByUserId")]
        public IEnumerable<Notes> GetNotesByUserId()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.GetNote(userId);
                if (result != null)
                    return result;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Get All Notes Api
        [HttpGet("GetAllNotes")]
        public List<Notes> GetAllNotes()
        {
            try
            {
                var result = notesBL.GetAllNote();
                if (result != null)
                    return result;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Check Archieve
        [Authorize]
        [HttpPost("IsArchieveOrNot")]
        public IActionResult IsArchieveOrNot(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.IsArchieveOrNot(noteId, userId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "  Is Trash Or Not checked ", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Unsuccessful" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Check Trash
        [Authorize]
        [HttpPost("IsTrashOrNot")]
        public IActionResult IsTrashOrNot(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.IsTrashOrNot(noteId, userId);
                if (result != null)
                    return this.Ok(new { Success = true, message = " Is Trash Or Not checked ", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Unsuccessful" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Check Pin
        [Authorize]
        [HttpPost("IsPinOrNot")]
        public IActionResult IsPinOrNot(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.IsPinOrNot(noteId, userId);
                if (result != null)
                    return this.Ok(new { Success = true, message = " Is Pin Or Not checked ", data = result });
                else
                    return this.BadRequest(new { Success = false, message = " Unsuccessful " });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Do Color
        [Authorize]
        [HttpPost("DoColour")]
        public IActionResult DoColour(long noteId , string color)
        {
            try
            {
                var result = notesBL.DoColour(noteId , color);
                if (result != null)
                    return this.Ok(new { Success = true, message = " Notes Coloured  successfully ", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Colour Unsuccessful" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Upload Image
        [Authorize]
        [HttpPost("UploadImage")]
        public IActionResult UploadImage(long noteId, IFormFile image)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.UploadImage(noteId, userId, image);
                if (result != null)
                    return this.Ok(new { Success = true, message = " Image Uploaded Successfully ", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Image Upload Failed ! Try Again " });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Create a Copy Of  Notes
        [Authorize]
        [HttpPost("MakeCopyOfNote")]
        public IActionResult MakeCopyOfNote(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.MakeCopyOfNote(noteId, userId);
                if (result != null)
                    return this.Ok(new { Success = true, message = " Created a Copy Of Note Successfully ", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Failed to create a Copy of  Note ! Try Again " });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
