using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL: INoteBL
    {
        private readonly INoteRL noteRL;
        //Constructor
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        //Method to return create note obj to Repository Layer notes.
        public Notes CreateNote(Note note, long userId)
        {
            try
            {
                return noteRL.CreateNote(note,userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Method to return update notes obj to Repository Layer notes
        public Notes UpdateNote(UpdateNote updateNote, long userId)
        {
            try
            {
                return noteRL.UpdateNote(updateNote, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteNotes(long id, long noteId)
        {
            try
            {

                return noteRL.DeleteNotes(id, noteId); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Notes> RetrieveAllNotes(long userId)
        {
            try
            {

                return noteRL.RetrieveAllNotes(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ChangeColor(long noteId, long userId, Note notesModel)
        {
            try
            {

                return noteRL.ChangeColor(noteId, userId, notesModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsPinned(long noteId, long userId)
        {
            try
            {
                return noteRL.IsPinned(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsTrash(long noteId, long userId)
        {
            try
            {
                return noteRL.IsTrash(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsArchive(long noteId, long userId)
        {
            try
            {
                return noteRL.IsArchive(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Notes> GetTrash(long userId)
        {
            try
            {
                return noteRL.GetTrash(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public List<Notes> GetArchived(long userId)
        {
            try
            {
                return noteRL.GetArchived(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Notes AddImage(long noteId, long userId, IFormFile formFile)
        {
            try
            {
                return noteRL.AddImage(noteId, userId, formFile);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteForever(long noteId, long userId)
        {
            try
            {
                return noteRL.DeleteForever(noteId, userId);
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
                return noteRL.Restore(noteId, userId);
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
                return noteRL.UnArchive(noteId, userId);
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
                return noteRL.AddRemainder(noteId, userId, dateTime);
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
                return noteRL.DeleteRemainder(noteId, userId);
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
                return noteRL.ChangeRemainder(noteId, userId, dateTime);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
