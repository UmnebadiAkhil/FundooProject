using BusinessLayer.Interface;
using CommonLayer.Model;
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
    }
}
