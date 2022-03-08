using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    //Business Layer Collab interface
    public interface ICollabBL
    {
        public Collabarator AddCollab(string email, long userId, long noteId);
        public Collabarator RemoveCollab(long userId, long collabId);
        public IEnumerable<Collabarator> GetByNoteId(long noteId);
        public IEnumerable<Collabarator> GetAllCollab();
    }
}
