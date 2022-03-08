using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollaborationBL:ICollaborationBL
    {
        private ICollaborationRL collaborationRL;
        private INoteRL noteRL;
        private IUserRL userRL;

        public CollaborationBL(ICollaborationRL collaborationRL, INoteRL noteRL, IUserRL userRL)
        {
            this.collaborationRL = collaborationRL;
            this.noteRL = noteRL;
            this.userRL = userRL;
        }

        public bool AddCollab(long noteId, long userId, string collabEmail)
        {
            try
            {
                var checkNote = noteRL.GetNoteId(noteId, userId);
                var checkEmail = userRL.GetEmail(collabEmail);

                if (checkNote == null)
                {
                    return false;
                }

                if (checkEmail == null)
                {
                    return false;
                }
                return collaborationRL.AddCollab(noteId, userId, collabEmail);
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
                return collaborationRL.GetCollab(noteId, userId);
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
                return collaborationRL.RemoveCollab(noteId, userId, collabEmail);
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
                return collaborationRL.GetAllCollab();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
