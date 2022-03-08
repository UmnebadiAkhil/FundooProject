using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollaborationRL: ICollaborationRL
    {
        private FundooContext fundooContext;
        Collaboration collaboration = new Collaboration();

        public CollaborationRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public bool AddCollab(long noteId, long userId, string collabEmail)
        {
            try
            {
                var result = fundooContext.CollabTable.FirstOrDefault(e => e.NotesId == noteId
                                                                            && e.Id == userId
                                                                            && e.CollabEmail == collabEmail);
                if (result == null)
                {
                    collaboration.CollabEmail = collabEmail;
                    collaboration.Id = userId;
                    collaboration.NotesId = noteId;
                    fundooContext.CollabTable.Add(collaboration);

                    var changes = fundooContext.SaveChanges();

                    if (changes > 0) return true;

                    else return false;
                }

                else return false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Collaboration> GetCollab(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.CollabTable.Where(e => e.NotesId == noteId && e.Id == userId).ToList();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveCollab(long noteId, long userId, string collabEmail)
        {
            try
            {
                var result = fundooContext.CollabTable.FirstOrDefault(e => e.NotesId == noteId
                                                                            && e.Id == userId
                                                                            && e.CollabEmail == collabEmail);

                if (result != null)
                {
                    fundooContext.CollabTable.Remove(result);
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

        public IEnumerable<Collaboration> GetAllCollab()
        {
            try
            {
                //Fetch All the details from Collab Table
                var collabs = fundooContext.CollabTable.ToList();
                if (collabs != null)
                {
                    return collabs;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
