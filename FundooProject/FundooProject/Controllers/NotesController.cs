using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL noteBL;
        //Constructor
        public NotesController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }

        private long GetTokenId()
        {
            return Convert.ToInt64(User.FindFirst("Id").Value);
        }

        //Create a Note
        [Authorize]
        [HttpPost("CreateNote")]
        public IActionResult CreateNote(Note note)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = noteBL.CreateNote(note, userId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Notes created successful", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Notes not created " });
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Update a Note
        [Authorize]
        [HttpPut("UpdateNote")]
        public IActionResult UpdateNote(UpdateNote note, long noteId)
        {
            try
            {
                var result = noteBL.UpdateNote(note, noteId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Notes updated successful", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Update failed" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Delete note
        [Authorize]
        [HttpDelete("DeleteNotes")]
        public IActionResult DeleteNotes(long id,long noteId)
        {
            try
            {
                if (noteBL.DeleteNotes(id, noteId))
                    return this.Ok(new { Success = true, message = "Deleted successful", data = noteBL.DeleteNotes(id, noteId) });
                else
                    return this.BadRequest(new { Success = false, message = "Notes not deleted " });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Retrieve note
        [Authorize]
        [HttpGet("RetrieveAllNotes")]
        public IActionResult RetrieveAllNotes(long id)
        {
            try
            {
                var result = noteBL.RetrieveAllNotes(id);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Retrieve successful", data = noteBL.RetrieveAllNotes(id) });
                else
                    return this.BadRequest(new { Success = false, message = "Failed! " });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Change colour
        [Authorize]
        [HttpPut("Colour")]
        public IActionResult ChangeColor(long noteId, Note notesModel)
        {
            long userId = GetTokenId();
            bool result = noteBL.ChangeColor(noteId, userId, notesModel);

            try
            {
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Color changed Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Color not changed !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        //IsArchieve
        [Authorize]
        [HttpGet("Archived")]
        public IActionResult GetArchived()
        {
            try
            {
                long userId = GetTokenId();
                var archivedList = noteBL.GetArchived(userId);

                if (archivedList.Count != 0)
                {
                    return this.Ok(new { Success = true, message = "These are your Archived Notes.", Data = archivedList });
                }
                else if (archivedList.Count == 0)
                {
                    return Ok(new { Success = true, message = "There are no Archived notes." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        //IsTrashed 
        [Authorize]
        [HttpGet("Trashed")]
        public IActionResult GetTrash()
        {
            try
            {
                long userId = GetTokenId();
                var trashList = noteBL.GetTrash(userId);

                if (trashList.Count != 0)
                {
                    return this.Ok(new { Success = true, message = "These are your Trashed Notes.", Data = trashList });
                }
                else if (trashList.Count == 0)
                {
                    return Ok(new { Success = true, message = "There are no trashed notes." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        //IsPinned
        [Authorize]
        [HttpGet("Pin")]
        public IActionResult IsPinned(long noteId)
        {
            long userId = GetTokenId();
            bool result = noteBL.IsPinned(noteId, userId);

            try
            {
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Successful" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Upload Image
        [Authorize]
        [HttpPost("UploadImage")]
        public IActionResult AddImage(long noteId, IFormFile formFile)
        {
            try
            {
                long userId = GetTokenId();
                bool result = noteBL.AddImage(noteId, userId, formFile);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Image added Successfully." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Failed to add image." });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
 }

