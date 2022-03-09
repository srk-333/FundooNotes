//-----------------------------------------------------------------------
// <copyright file="LabelsController.cs" company="Saurav">
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
    ///  controller for Labels
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        /// <summary>
        /// The label interface
        /// </summary>
        private readonly ILabelBL labelBL;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelsController"/> class.
        /// </summary>
        /// <param name="labelBL">The label interface.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public LabelsController(ILabelBL labelBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Adds the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Label details </returns>
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddLabelName(string labelName, long noteId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var label = this.labelBL.AddLabelName(labelName, noteId, userId);
                if (label != null)
                {
                    return this.Ok(new { Success = true, message = " Collab Added  successfully ", data = label });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Collab Add Failed ! Try Again" });
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
        /// <returns>updated label name </returns>
        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateLabelName(string labelName, long noteId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var notes = this.labelBL.UpdateLabelName(labelName, noteId, userId);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = " Label Name Updated  successfully ", data = notes });
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
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns> true or false </returns>
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveLabel(long labelId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.labelBL.RemoveLabel(labelId, userId))
                {
                    return this.Ok(new { Success = true, message = " Label Removed  successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Label Remove Failed ! Try Again" });
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
        /// <returns> label by note id </returns>
        [Authorize]
        [HttpGet("{Id}/{NotesId}/Get")]
        public IEnumerable<Labels> GetByNoteId(long noteId)
        {
            try
            {
                var result = this.labelBL.GetByNoteId(noteId);
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
        /// Gets the by user identifier.
        /// </summary>
        /// <returns> label by user id </returns>
        [Authorize]
        [HttpGet("{Id}/Get")]
        public IEnumerable<Labels> GetByUserId()
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.labelBL.GetByUserId(userId);
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
        /// Gets all labels.
        /// </summary>
        /// <returns>All label details </returns>
        [HttpGet("GetAll")]
        public IEnumerable<Labels> GetAllLabels()
        {
            try
            {
                var result = this.labelBL.GetAllLabels();
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
        /// Gets all labels using cache.
        /// </summary>
        /// <returns> Label details from cache </returns>
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllLabesUsingRedisCache()
        {
            var cacheKey = "labelList";
            string serializedLabelList;
            var labelsList = new List<Labels>();
            var redisLabelList = await this.distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelsList = JsonConvert.DeserializeObject<List<Labels>>(serializedLabelList);
            }
            else
            {
                labelsList = (List<Labels>)this.labelBL.GetAllLabels();
                serializedLabelList = JsonConvert.SerializeObject(labelsList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(15))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await this.distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }

            return this.Ok(labelsList);
        }
    }
}
