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
    public class LabelsController : ControllerBase
    {
        //instance of BusinessLayer Interface
        private readonly ILabelBL labelBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        //Constructor
        public LabelsController(ILabelBL labelBL , IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        // Add Label Name Api
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddLabelName(string labelName, long noteId)
        {
            try
            {
                //Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var label = labelBL.AddLabelName(labelName, noteId , userId);
                if (label != null)
                    return this.Ok(new { Success = true, message = " Collab Added  successfully ", data = label });
                else
                    return this.BadRequest(new { Success = false, message = "Collab Add Failed ! Try Again" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Update Label Name Api
        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateLabelName(string labelName, long noteId)
        {
            try
            {
                //Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var notes = labelBL.UpdateLabelName(labelName, noteId , userId);
                if (notes != null)
                    return this.Ok(new { Success = true, message = " Label Name Updated  successfully ", data = notes });
                else
                    return this.BadRequest(new { Success = false, message = "Failed! Didn't updated" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Remove Collab
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveLabel(long labelId)
        {
            try
            {
                //Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (labelBL.RemoveLabel(labelId , userId))
                    return this.Ok(new { Success = true, message = " Collab Removed  successfully "});
                else
                    return this.BadRequest(new { Success = false, message = "Collab Remove Failed ! Try Again" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Get Labels by NoteId
        [Authorize]
        [HttpGet("Get")]
        public IEnumerable<Labels> GetByNoteId(long noteId)
        {
            try
            {
                var result = labelBL.GetByNoteId(noteId);
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
        //Get Labels by UserId
        [Authorize]
        [HttpGet("{Id}/Get")]
        public IEnumerable<Labels> GetByUserId()
        {
            try
            {
                //Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.GetByUserId(userId);
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
        //Get All  Labels 
        [HttpGet("GetAll")]
        public IEnumerable<Labels> GetAllLabels()
        {
            try
            {
                var result = labelBL.GetAllLabels();
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
        public async Task<IActionResult> GetAllLabesUsingRedisCache()
        {
            var cacheKey = "labelList";
            string serializedLabelList;
            var labelsList = new List<Labels>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelsList = JsonConvert.DeserializeObject<List<Labels>>(serializedLabelList);
            }
            else
            {
                labelsList = (List<Labels>)labelBL.GetAllLabels();
                serializedLabelList = JsonConvert.SerializeObject(labelsList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(15))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(labelsList);
        }
    }
}
