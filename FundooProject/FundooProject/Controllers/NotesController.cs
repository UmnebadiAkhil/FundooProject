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
using static System.Net.Mime.MediaTypeNames;

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
        [HttpPost("Create")]
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
        [HttpPut("Update")]
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
        [HttpDelete("Delete")]
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
        [HttpGet("Retrieve")]
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
        [HttpPut("Archived")]
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
        [HttpPut("Trashed")]
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
        [HttpPut("Pin")]
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
        [HttpPost("Upload")]
        public IActionResult AddImage(long noteId, IFormFile formFile )
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = noteBL.AddImage(noteId, userId, formFile);

                if (result != null)
                {
                    return Ok(new { Success = true, message = "Image added Successfully.", data = result });
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

        //Trash/Delete
        [Authorize]
        [HttpDelete("Trash/Delete")]
        public IActionResult DeleteForever(long noteId)
        {
            try
            {
                long userId = GetTokenId();
                bool result = noteBL.DeleteForever(noteId, userId);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Deleted forever from trash Successfully." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Note deletion failed." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        //Reminder
        [Authorize]
        [HttpDelete("Reminder")]
        public IActionResult DeleteRemainder(long noteId)
        {
            try
            {
                long userId = GetTokenId();
                bool result = noteBL.DeleteRemainder(noteId, userId);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Note Reminder removed Successfully." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "No Reminder is added to this note." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        //Archive/Unarchive
        [Authorize]
        [HttpPut("Archive/Unarchive")]
        public IActionResult Unarchive(long noteId)
        {
            try
            {
                long userId = GetTokenId();
                bool result = noteBL.UnArchive(noteId, userId);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Note Unarchived Successfully." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }

        //Reminder
        [Authorize]
        [HttpPut("Reminder")]
        public IActionResult ChangeRemainder(long noteId, DateTime dateTime)
        {
            try
            {
                long userId = GetTokenId();
                bool result = noteBL.ChangeRemainder(noteId, userId, dateTime);

                if (result == true)
                {
                    return Ok(new { Success = true, message = "Note Reminder changed Successfully." });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "No Reminder is added to this note." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }
    }
 }

