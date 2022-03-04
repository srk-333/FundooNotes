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
        [HttpPost("Create")]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                //Take id of  Logged In User
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
        [HttpPut("Update")]
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
        [HttpDelete("Delete")]
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
        [HttpGet("{Id}/Get")]
        public IEnumerable<Notes> GetNotesByUserId()
        {
            try
            {
                //Take id of  Logged In User
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
        [HttpGet("GetAll")]
        public IEnumerable<Notes> GetAllNotes()
        {
            try
            {
                var result = notesBL.GetAllNotes();
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
        [HttpPost("IsArchieve")]
        public IActionResult IsArchieveOrNot(long noteId)
        {
            try
            {
                //Take id of  Logged In User
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
        [HttpPut("IsTrash")]
        public IActionResult IsTrashOrNot(long noteId)
        {
            try
            {
                //Take id of  Logged In User
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
        [HttpPut("IsPin")]
        public IActionResult IsPinOrNot(long noteId)
        {
            try
            {
                //Take id of  Logged In User
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
        [HttpPut("Colour")]
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
        [HttpPost("Image")]
        public IActionResult UploadImage(long noteId, IFormFile image)
        {
            try
            {
                //Take id of  Logged In User
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
        [HttpPost("MakeCopy")]
        public IActionResult MakeCopyOfNote(long noteId)
        {
            try
            {
                //Take id of  Logged In User
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
