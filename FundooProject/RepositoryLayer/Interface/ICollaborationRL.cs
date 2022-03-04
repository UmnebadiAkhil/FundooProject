using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollaborationRL
    {

        public bool AddCollab(long noteId, long userId, string collabEmail);

        public List<Collaboration> GetCollab(long noteId, long userId);

        public bool RemoveCollab(long noteId, long userId, string collabEmail);
    }
}
