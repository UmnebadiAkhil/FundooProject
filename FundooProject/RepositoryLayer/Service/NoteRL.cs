using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace RepositoryLayer.Service
{
    public class NoteRL:INoteRL
    {
        //instance of  FundooContext Class
        private readonly FundooContext fundooContext;
        private IConfiguration _config;

        //Constructor
        public NoteRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this._config = configuration;

        }
        //Method to Notes Details.
        public Notes CreateNote(Note note, long userId)
        {
            try
            {
                Notes newNotes = new Notes();
                newNotes.Title = note.Title;
                newNotes.Description = note.Description;
                newNotes.Color = note.Color;
                newNotes.Image = note.Image;
                newNotes.IsArchieve = note.IsArchieve;
                newNotes.IsTrash = note.IsTrash;
                newNotes.IsPin = note.IsPin;
                newNotes.CreatedAt = note.CreatedAt;
                newNotes.ModifiedAt = note.ModifiedAt;
                newNotes.Id = userId;
                fundooContext.NotesTable.Add(newNotes);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                    return newNotes;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to UpdateNote Details.
        public Notes UpdateNote(UpdateNote updateNote, long noteId)
        {
            try
            {
                var note = fundooContext.NotesTable.Where(update => update.NotesId == noteId ).FirstOrDefault();
                if (note != null)
                {
                    note.Title = updateNote.Title;
                    note.Description = updateNote.Description;
                    note.Color = updateNote.Color;
                    note.Image = updateNote.Image;
                    note.ModifiedAt = updateNote.ModifiedAt;
                    note.Id = noteId;
                    fundooContext.NotesTable.Update(note);
                    int result = fundooContext.SaveChanges();
                    return note;
                }
                    
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to DeleteNotes Details.
        public bool DeleteNotes(long id, long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(e => e.Id == id && e.NotesId == noteId).FirstOrDefault();

                if (result != null)
                {
                    fundooContext.NotesTable.Remove(result);
                    fundooContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to RetrieveAllNotes Details.
        public IEnumerable<Notes> RetrieveAllNotes(long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(e => e.Id == userId).ToList();
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

        //Method to ChangeColor Details.
        public bool ChangeColor(long noteId, long userId, Note notesModel)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId);

                if (result != null)
                {
                    result.Color = notesModel.Color;
                    result.ModifiedAt = DateTime.Now;
                }
                int changes = fundooContext.SaveChanges();

                if (changes > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to IsPinned Details.
        public bool IsPinned(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId);

                if (result != null)
                {
                    if (result.IsPin == true)
                    {
                        result.IsPin = false;
                    }
                    else if (result.IsPin == false)
                    {
                        result.IsPin = true;
                    }
                    result.ModifiedAt = DateTime.Now;
                }
                int changes = fundooContext.SaveChanges();

                if (changes > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to IsTrash Details.
        public bool IsTrash(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId);

                if (result != null)
                {
                    result.IsTrash = true;
                    result.IsArchieve = false;

                    result.ModifiedAt = DateTime.Now;
                }
                int changes = fundooContext.SaveChanges();

                if (changes > 0) { return true; }

                else { return false; }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to IsArchive Details.
        public bool IsArchive(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId);

                if (result != null)
                {
                    result.IsArchieve = true;
                    result.IsTrash = false;

                    result.ModifiedAt = DateTime.Now;
                }
                int changes = fundooContext.SaveChanges();

                if (changes > 0) return true;

                else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to GetTrash Details.
        public List<Notes> GetTrash(long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(e => e.Id == userId && e.IsTrash == true).ToList();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to GetArchived Details.
        public List<Notes> GetArchived(long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(e => e.Id == userId && e.IsArchieve == true).ToList();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to AddImage Details.
        public Notes AddImage(long noteId, long userId, IFormFile formFile)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId);

                if (result != null)
                {
                    Account account = new Account(_config["CloudinaryAccount:CloudName"],
                                                  _config["CloudinaryAccount:ApiKey"],
                                                  _config["CloudinaryAccount:ApiSecret"]);

                    Cloudinary cloudinary = new Cloudinary(account);
                    var imagePath = formFile.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(formFile.FileName, imagePath),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    result.Image = formFile.FileName;
                    fundooContext.NotesTable.Update(result);
                    int upload = fundooContext.SaveChanges();
                    if (upload > 0)
                        return result;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to GetNoteId Details.
        public Notes GetNoteId(long noteId, long userId)
        {
            var result = fundooContext.NotesTable.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId);

            return result;
        }

        public bool DeleteForever(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(note => note.IsTrash == true
                                                                    && note.Id == userId
                                                                    && note.NotesId == noteId);

                if (result != null)
                {
                    fundooContext.NotesTable.Remove(result);
                    fundooContext.SaveChanges();

                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Restore(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId);

                if (result != null)
                {
                    result.IsTrash = false;
                    result.IsArchieve = false;

                    result.ModifiedAt = DateTime.Now;
                }
                int changes = fundooContext.SaveChanges();

                if (changes > 0) { return true; }

                else { return false; }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UnArchive(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId);

                if (result != null)
                {
                    result.IsArchieve = false;
                    result.IsTrash = false;

                    result.ModifiedAt = DateTime.Now;
                }
                int changes = fundooContext.SaveChanges();

                if (changes > 0) return true;

                else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddRemainder(long noteId, long userId, DateTime dateTime)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId && e.Reminder == null);

                if (result != null)
                {
                    result.Reminder = dateTime;

                    result.ModifiedAt = DateTime.Now;
                }
                int changes = fundooContext.SaveChanges();

                if (changes > 0) return true;

                else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteRemainder(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId && e.Reminder != null);

                if (result != null)
                {
                    
                    result.ModifiedAt = DateTime.Now;
                }
                int changes = fundooContext.SaveChanges();

                if (changes > 0) return true;

                else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ChangeRemainder(long noteId, long userId, DateTime dateTime)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId
                                                                 && e.IsArchieve == false && e.IsTrash == false);

                if (result != null)
                {
                    result.Reminder = dateTime;

                    result.ModifiedAt = DateTime.Now;
                }
                int changes = fundooContext.SaveChanges();

                if (changes > 0) return true;

                else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
