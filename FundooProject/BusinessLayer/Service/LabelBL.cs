using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL:ILabelBL
    {
        private ILabelRL labelRL;

        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        public bool AddLabelName(long noteId, long userId, LabelModel labelModel)
        {
            try
            {
                return labelRL.AddLabelName(noteId, userId, labelModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public List<Label> GetNoteLables(long noteId, long userId)
        {
            try
            {
                return labelRL.GetNoteLables(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Label> GetUserLabels(long userId)
        {
            try
            {
                return labelRL.GetUserLabels(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EditLabelName(long labelId, long userId, LabelModel labelModel)
        {
            try
            {
                return labelRL.EditLabelName(labelId, userId, labelModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveLabel(long labelId, long noteId, long userId)
        {
            try
            {
                return labelRL.RemoveLabel(labelId, noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteLabel(long userId, string labelName)
        {
            try
            {
                return labelRL.DeleteLabel(userId, labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddLabelToUser(long userId, LabelModel labelModel)
        {
            try
            {
                return labelRL.AddLabelToUser(userId, labelModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AddNoteToExistingLabel(long noteId, long userId, string labelName)
        {
            try
            {
                return labelRL.AddNoteToExistingLabel(noteId, userId, labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
