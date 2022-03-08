using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaborationController : ControllerBase
    {
        private ICollaborationBL collaborationBL;

        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;

        public CollaborationController(ICollaborationBL collaborationBL, IDistributedCache distributedCache, IMemoryCache memoryCache)
        {
            this.collaborationBL = collaborationBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        private long GetTokenId()
        {
            return Convert.ToInt64(User.FindFirst("Id").Value);
        }

        
        
        [HttpPost("Add")]
        public IActionResult AddCollab(long noteId, CollaborationModel collaboration)
        {
            try
            {
                long userId = GetTokenId();
                var result = collaborationBL.AddCollab(noteId, userId, collaboration.collabEmail);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Collaboration Added Successfully." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Adding Collaboration failed." });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [Authorize]
        [HttpGet("Get")]
        public IActionResult GetCollab(long noteId)
        {
            try
            {
                long userId = GetTokenId();
                var collabList = collaborationBL.GetCollab(noteId, userId);

                if (collabList.Count != 0)
                {
                    return Ok(new { success = true, message = "These are the Collaborations of these note.", data = collabList });
                }
                else if (collabList.Count == 0)
                {
                    return Ok(new { Success = true, message = "No collaboration found." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Something went wrong." });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetAll")]
        public IEnumerable<Collaboration> GetAllCollab()
        {
            try
            {
                var result = collaborationBL.GetAllCollab();
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
            var collabList = new List<Collaboration>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedcollabList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<Collaboration>>(serializedcollabList);
            }
            else
            {
                collabList = (List<Collaboration>)collaborationBL.GetAllCollab();
                serializedcollabList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedcollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(15))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(collabList);
        }

        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveCollab(long noteId, CollaborationModel collaborationModel)
        {
            try
            {
                long userId = GetTokenId();
                var result = collaborationBL.RemoveCollab(noteId, userId, collaborationModel.collabEmail);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Collaboration Removed Successfully." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Removing Collaboration failed." });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
