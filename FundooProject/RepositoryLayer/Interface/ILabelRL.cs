using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public bool AddLabelName(long noteId, long userId, LabelModel labelModel);
        public List<Label> GetNoteLables(long noteId, long userId);

        public List<Label> GetUserLabels(long userId);

        public bool EditLabelName(long labelId, long userId, LabelModel labelModel);

        public bool RemoveLabel(long labelId, long noteId, long userId);

        public bool DeleteLabel(long userId, string labelName);

        public bool AddLabelToUser(long userId, LabelModel labelModel);

        public bool AddNoteToExistingLabel(long noteId, long userId, string labelName);
    }
}
