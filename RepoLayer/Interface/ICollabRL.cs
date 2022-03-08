using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    // Collab interface
    public interface ICollabRL
    {
        public Collabarator AddCollab(string email, long userId, long noteId);
        public Collabarator RemoveCollab(long userId, long collabId);
        public IEnumerable<Collabarator> GetByNoteId(long noteId);
        public IEnumerable<Collabarator> GetAllCollab();
    }
}
