using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaborationController : ControllerBase
    {
        private ICollaborationBL collaborationBL;

        public CollaborationController(ICollaborationBL collaborationBL)
        {
            this.collaborationBL = collaborationBL;
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
