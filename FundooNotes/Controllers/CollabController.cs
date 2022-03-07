using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        //instance of BusinessLayer Interface
        private readonly ICollabBL collabBL;
        //Constructor
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        // Add Collab
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCollab(string email, long noteId)
        {
            try
            {
                //Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = collabBL.AddCollab(email, userId, noteId);
                if (result != null)
                    return this.Ok(new { Success = true, message = " Collab Added  successfully ", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Collab Add Failed ! Try Again" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        //Remove Collab
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveCollab(long collabId)
        {
            try
            {
                //Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = collabBL.RemoveCollab(userId, collabId);
                if (result != null)
                    return this.Ok(new { Success = true, message = " Collab Removed  successfully ", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Collab Remove Failed ! Try Again" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        //Get Collab by NoteId
        [Authorize]
        [HttpGet("Get/{NoteId}")]
        public IEnumerable<Collabarator> GetByNoteId(long noteId)
        {
            try
            {
                var result = collabBL.GetByNoteId(noteId);
                if (result != null)
                    return result;
                else
                    return null;
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
