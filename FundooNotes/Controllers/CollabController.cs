using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        //instance of BusinessLayer Interface
        private readonly ICollabBL collabBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        //Constructor
        public CollabController(ICollabBL collabBL , IMemoryCache memoryCache , IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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
        [HttpGet("Get")]
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
        //Get All  Collab 
        [HttpGet("GetAll")]
        public IEnumerable<Collabarator> GetAllCollab()
        {
            try
            {
                var result = collabBL.GetAllCollab();
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
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            var cacheKey = "collabList";
            string serializedcollabList;
            var collabList = new List<Collabarator>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedcollabList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<Collabarator>>(serializedcollabList);
            }
            else
            {
                collabList = (List<Collabarator>)collabBL.GetAllCollab();
                serializedcollabList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedcollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(15))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(collabList);
        }
    }
}
