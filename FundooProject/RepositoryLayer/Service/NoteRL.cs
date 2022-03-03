using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRL:INoteRL
    {
        //instance of  FundooContext Class
        private readonly FundooContext fundooContext;
     
        //Constructor
        public NoteRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
            
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

        public bool ChangeColor(long noteId, long userId, Note notesModel)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(e => e.Id == noteId && e.Id == userId);

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

    }
}
