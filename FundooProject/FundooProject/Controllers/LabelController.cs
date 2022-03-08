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
    public class LabelController : ControllerBase
    {
        private ILabelBL labelBL;

        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;
        public LabelController(ILabelBL labelBL, IDistributedCache distributedCache, IMemoryCache memoryCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        private long GetTokenId()
        {
            return Convert.ToInt64(User.FindFirst("Id").Value);
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddLabelName(long noteId, LabelModel labelModel)
        {
            try
            {
                long Id = GetTokenId();
                var result = labelBL.AddLabelName(noteId, Id, labelModel);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Label added Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Label already exists !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message });
            }
        }

        [Authorize]
        [HttpGet("GetNoteLabels")]
        public IActionResult GetNoteLables(long noteId)
        {
            try
            {
                long userId = GetTokenId();
                var labelList = labelBL.GetNoteLables(noteId, userId);

                if (labelList.Count != 0)
                {
                    return this.Ok(new { Success = true, message = "These are Your all the Labels.", Data = labelList });
                }
                else if (labelList.Count == 0)
                {
                    return BadRequest(new { Success = false, message = "No Label is added to this note." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message });
            }
        }

        [Authorize]
        [HttpGet("GetUserLabels")]
        public IActionResult GetUserLabels()
        {
            try
            {
                long userId = GetTokenId();
                List<Label> labelList = labelBL.GetUserLabels(userId);

                if (labelList.Count != 0)
                {
                    return this.Ok(new { Success = true, message = "These are Your all the Labels.", Data = labelList });
                }
                else if (labelList.Count == 0)
                {
                    return BadRequest(new { Success = false, message = "No Label is added." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message });
            }
        }

        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "customerList";
            string serializedCustomerList;
            var customerList = new List<Label>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<Label>>(serializedCustomerList);
            }
            else
            {
                long userId = GetTokenId();
                customerList = labelBL.GetUserLabels(userId);
                serializedCustomerList = JsonConvert.SerializeObject(customerList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(customerList);
        }

        [Authorize]
        [HttpPut("EditLabelName")]
        public IActionResult EditLabelName(long labelId, LabelModel labelModel)
        {
            try
            {
                long userId = GetTokenId();
                bool result = labelBL.EditLabelName(labelId, userId, labelModel);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Label changed Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Label does not exists !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message });
            }
        }

        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveLabelFromNote(long labelId, long noteId)
        {
            try
            {
                long userId = GetTokenId();
                bool result = labelBL.RemoveLabel(labelId, noteId, userId);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Label Removed Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Failed to Remove label !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message });
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteLabel(LabelModel labelModel)
        {
            try
            {
                long userId = GetTokenId();
                bool result = labelBL.DeleteLabel(userId, labelModel.labelName);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Label Removed Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Failed to Remove label !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message });
            }
        }

        [Authorize]
        [HttpPost("AddLabelToUser")]
        public IActionResult AddLabelToUser(LabelModel labelModel)
        {
            try
            {
                long userId = GetTokenId();
                bool result = labelBL.AddLabelToUser(userId, labelModel);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Label added Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Label already exists !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message });
            }
        }
        [Authorize]
        [HttpPost("AddNoteToExistingLabel")]
        public IActionResult AddNoteToExistingLabel(long noteId, string labelName)
        {
            try
            {
                LabelModel labelModel = new LabelModel();
                long userId = GetTokenId();
                bool result = labelBL.AddNoteToExistingLabel(noteId, userId, labelName);

                if (result == true)
                {
                    return Ok(new { Success = true, message = $"Note added to {labelName} Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Label doesn't exists !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message });
            }
        }


    }
}
