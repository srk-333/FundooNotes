using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    // Notes interface
    public interface INotesRL
    {
        public Notes CreateNote(NotesModel notesModel, long userId);
        public Notes UpdateNote(UpdateNote notesModel, long noteId);
        public bool DeleteNote(long noteId);
        public IEnumerable<Notes> GetNote(long userId);
        public IEnumerable<Notes> GetAllNote();
    }
}
