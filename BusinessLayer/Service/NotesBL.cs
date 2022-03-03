using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    //Service Class 
    public class NotesBL : INotesBL
    {
        //instance of RepoLayer Interface
        private readonly INotesRL notesRL;
        //Constructor
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        //Create Notes in database
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
        //Update Notes by NoteId in database
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
        //Delete Notes by NoteId from database
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
        //Get Notes By UserId from database
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
        ///Get All Notes from database
        public IEnumerable<Notes> GetAllNote()
        {
            try
            {
                return notesRL.GetAllNote();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
