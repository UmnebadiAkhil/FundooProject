using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;
        Label label = new Label();
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public bool AddLabelName(long noteId, long userId, LabelModel labelModel)
        {
            try
            {
                var checkLabel = fundooContext.LabelTable.FirstOrDefault(l => l.LabelName == labelModel.labelName && l.NotesId == noteId);

                if (checkLabel == null)
                {
                    label.LabelName = labelModel.labelName;
                    label.NotesId = noteId;
                    label.Id = userId;

                    fundooContext.LabelTable.Add(label);
                    int changes = fundooContext.SaveChanges();

                    if (changes > 0) return true;

                    else return false;
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

        public List<Label> GetNoteLables(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(l => l.NotesId == noteId && l.Id == userId).ToList();

                return result;
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
                var result = fundooContext.LabelTable.Where(l => l.Id == userId).ToList();

                return result;
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
                var result = fundooContext.LabelTable.FirstOrDefault(e => e.LabelId == labelId && e.Id == userId);
                var checkLabel = fundooContext.LabelTable.FirstOrDefault(l => l.LabelName == labelModel.labelName && l.Id == userId);

                if (result != null && checkLabel == null)
                {
                    result.LabelName = labelModel.labelName;

                    fundooContext.SaveChanges();

                    return true;
                }
                else return false;
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
                var result = fundooContext.LabelTable.FirstOrDefault(e => e.LabelId == labelId && e.Id == userId && e.NotesId == noteId);

                if (result != null)
                {
                    fundooContext.LabelTable.Remove(result);
                    fundooContext.SaveChanges();

                    return true;
                }
                else return false;
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
                var result = fundooContext.LabelTable.Where(e => e.Id == userId && e.LabelName == labelName).ToList();

                if (result != null)
                {
                    fundooContext.LabelTable.RemoveRange(result);
                    fundooContext.SaveChanges();

                    return true;
                }
                else return false;
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
                var checkLabel = fundooContext.LabelTable.FirstOrDefault(l => l.LabelName == labelModel.labelName && l.Id == userId);

                if (checkLabel == null)
                {
                    label.LabelName = labelModel.labelName;
                    label.Id = userId;

                    fundooContext.LabelTable.Add(label);
                    int changes = fundooContext.SaveChanges();

                    if (changes > 0) return true;

                    else return false;
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

        public bool AddNoteToExistingLabel(long noteId, long userId, string labelName)
        {
            try
            {
                var checkLabel = fundooContext.LabelTable.FirstOrDefault(l => l.LabelName == labelName && l.Id == userId);

                if (checkLabel != null)
                {
                    label.LabelName = labelName;
                    label.NotesId = noteId;
                    label.Id = userId;

                    fundooContext.LabelTable.Add(label);
                    int changes = fundooContext.SaveChanges();

                    if (changes > 0) return true;

                    else return false;
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
    }
}
