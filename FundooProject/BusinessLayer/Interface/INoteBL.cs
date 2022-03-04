using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public Notes CreateNote(Note note, long userId);
        public Notes UpdateNote(UpdateNote updateNote, long userId);
        public bool DeleteNotes(long id, long noteId);
        public IEnumerable<Notes> RetrieveAllNotes(long userId);
        public bool ChangeColor(long noteId, long userId, Note notesModel);
        public bool IsTrash(long noteId, long userId);
        public bool IsArchive(long noteId, long userId);
        public bool IsPinned(long noteId, long userId);
        public List<Notes> GetTrash(long userId);
        public List<Notes> GetArchived(long userId);
        public Notes AddImage(long noteId, long userId, IFormFile formFile);
        
    }
}
