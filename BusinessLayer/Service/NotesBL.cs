using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
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
        public List<Notes> GetAllNote()
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
        //Method to Check IsPArchieve or Not
        public Notes IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                return notesRL.IsArchieveOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Method to Check IsTrash or Not
        public Notes IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                return notesRL.IsTrashOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Method to Check IsPin or Not
        public Notes IsPinOrNot(long noteId, long userId)
        {
            try
            {
                return notesRL.IsPinOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to implement Colour
        public Notes DoColour(long noteId, string color)
        {
            try
            {
                return notesRL.DoColour(noteId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to upload Image
        public Notes UploadImage(long noteId, long userId, IFormFile image)
        {
            try
            {
                return notesRL.UploadImage(noteId, userId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to make a copy of Notes
        public Notes MakeCopyOfNote(long noteId, long userId)
        {
            try
            {
                return notesRL.MakeCopyOfNote(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
