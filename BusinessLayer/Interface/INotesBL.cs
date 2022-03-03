using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    //Business Layer Notes interface
    public interface INotesBL
    {
        public Notes CreateNote(NotesModel notesModel, long userId);
        public Notes UpdateNote(UpdateNote notesModel, long noteId);
        public bool DeleteNote(long noteId);
        public IEnumerable<Notes> GetNote(long userId);
        public List<Notes> GetAllNote();
        public Notes IsArchieveOrNot(long noteId, long userId);
        public Notes IsTrashOrNot(long noteId, long userId);
        public Notes IsPinOrNot(long noteId, long userId);
        public Notes DoColour(long noteId, string color);
        public Notes UploadImage(long noteId, long userId, IFormFile image);
        public Notes MakeCopyOfNote(long noteId, long userId);
    }
}
