using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        //instance of RepoLayer Interface
        private readonly INotesRL notesRL;
        //Constructor
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        //Create Notes
        public Notes CreateNote(NotesModel notesModel, long userId)
        {
            try
            {
                return notesRL.CreateNote(notesModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Update Notes
        public Notes UpdateNote(UpdateNote notesModel, long noteId)
        {
            try
            {
                return notesRL.UpdateNote(notesModel, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Delete Notes
        public bool DeleteNote(long noteId)
        {
            try
            {
                return notesRL.DeleteNote(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Get Notes By UserId
        public IEnumerable<Notes> GetNote(long userId)
        {
            try
            {
                return notesRL.GetNote(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
