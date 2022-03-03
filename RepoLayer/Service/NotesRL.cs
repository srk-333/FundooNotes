using CommonLayer.Models;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    //Service Class
    public class NotesRL : INotesRL
    {
        //instance of Classes
        private readonly FundooContext fundooContext;
        //Constructor
        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;       
        }
        //Method to Create a Note with some details.
        public Notes CreateNote(NotesModel notesModel, long userId)
        {
            try
            {
                Notes notes = new Notes();
                notes.Title = notesModel.Title;
                notes.Description = notesModel.Description;
                notes.Color = notesModel.Color;
                notes.Image = notesModel.Image;
                notes.IsArchieve = notesModel.IsArchieve;
                notes.IsTrash = notesModel.IsTrash;
                notes.IsPin = notesModel.IsPin;
                notes.CreateAt = notesModel.CreateAt;
                notes.ModifiedAt = notesModel.ModifiedAt;
                notes.Id = userId;
                //Add all the details in Notes Table.
                fundooContext.NotesTable.Add(notes);
                //Save Changes Made in the database
                int result = fundooContext.SaveChanges();
                if (result > 0)
                    return notes;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to Update the existing Notes in the system.
        public Notes UpdateNote(UpdateNote notesModel, long noteId)
        {
            try
            {
                //Fetch All the details with the given noteId.
                var note = fundooContext.NotesTable.Where(u => u.NotesId == noteId).FirstOrDefault();
                if (note != null)
                {
                    note.Title = notesModel.Title;
                    note.Description = notesModel.Description;
                    note.Color = notesModel.Color;
                    note.Image = notesModel.Image;
                    note.ModifiedAt = notesModel.ModifiedAt;
                    //Update database for given NoteId.
                    fundooContext.NotesTable.Update(note);
                    //Save Changes Made in the database
                    fundooContext.SaveChanges();
                    return note;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to Delete a Note from Database
        public bool DeleteNote(long noteId)
        {
            try
            {
                //Fetch All the details with the given noteId.
                var notes = fundooContext.NotesTable.Where(n => n.NotesId == noteId).FirstOrDefault();
                if (notes != null)
                {
                    //Remove Note details from database
                    fundooContext.NotesTable.Remove(notes);
                    //Save Changes Made in the database
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to Fetch Notes from Database for the given UserId.
        public IEnumerable<Notes> GetNote(long userId)
        {
            try
            {
                //Fetch All the details from Notes Table for the given UserId.
                var notes = fundooContext.NotesTable.Where(n => n.Id == userId).ToList();
                if (notes != null)
                {
                    return notes;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to Fetch All Notes from Database
        public IEnumerable<Notes> GetAllNote()
        {
            try
            {
                //Fetch All the details from Notes Table
                var notes = fundooContext.NotesTable.ToList();
                if (notes != null)
                {
                    return notes;
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
