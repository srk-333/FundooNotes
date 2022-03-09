//-----------------------------------------------------------------------
// <copyright file="CollabController.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using RepoLayer.Entity;
  
    /// <summary>
    /// controller class for Collaborator
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        /// <summary>
        /// The Collaborator interface of business layer
        /// </summary>
        private readonly ICollabBL collabBL;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabController"/> class.
        /// </summary>
        /// <param name="collabBL">The Collaborator of business layer</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Adds the Collaborator.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> Collaborator details </returns>
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCollab(string email, long noteId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collabBL.AddCollab(email, userId, noteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Collab Added  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Collab Add Failed ! Try Again" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Removes the Collaborator.
        /// </summary>
        /// <param name="collabId">The Collaborator identifier.</param>
        /// <returns> removed Collaborator details </returns>
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveCollab(long collabId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collabBL.RemoveCollab(userId, collabId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Collab Removed  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Collab Remove Failed ! Try Again" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns> Collaborator details by note id </returns>
        [Authorize]
        [HttpGet("{Id}/{NotesId}/Get")]
        public IEnumerable<Collabarator> GetByNoteId(long noteId)
        {
            try
            {
                var result = this.collabBL.GetByNoteId(noteId);
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
        /// Gets all Collaborator.
        /// </summary>
        /// <returns> all Collaborator details </returns>
        [HttpGet("GetAll")]
        public IEnumerable<Collabarator> GetAllCollab()
        {
            try
            {
                var result = this.collabBL.GetAllCollab();
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
        /// Gets all Collaborator using cache.
        /// </summary>
        /// <returns> Collaborator from Cache</returns>
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            var cacheKey = "collabList";
            string serializedcollabList;
            var collabList = new List<Collabarator>();
            var redisCollabList = await this.distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedcollabList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<Collabarator>>(serializedcollabList);
            }
            else
            {
                collabList = (List<Collabarator>)this.collabBL.GetAllCollab();
                serializedcollabList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedcollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(15))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await this.distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }

            return this.Ok(collabList);
        }
    }
}
