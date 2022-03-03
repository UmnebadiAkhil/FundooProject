using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public Notes CreateNote(Note note, long userId);
        public Notes UpdateNote(UpdateNote updateNote, long userId);
        public bool DeleteNotes(long id, long noteId);
        public IEnumerable<Notes> RetrieveAllNotes(long userId);
        public bool ChangeColor(long noteId, long userId, Note notesModel);
    }
}
