using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
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
        //Cloudinary Acc Info.
        private const string cloudName = "saurav-kr";
        private const string apiKey = "623933474268242";
        private const string apiSecret = "xpFVFqhxwtaTHq-b4hv8v_2eZLQ";
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
        public List<Notes> GetAllNote()
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
        //Method to Check IsPArchieve or Not
        public Notes IsArchieveOrNot(long noteId , long userId)
        {
            try
            {
                //Fetch All the details with the given noteId and userId
                var notes = fundooContext.NotesTable.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.IsArchieve == false)
                    {
                        notes.IsArchieve = true;
                        //Save Changes
                        fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsArchieve = false;
                        //Save Changes
                        fundooContext.SaveChanges();
                        return notes;
                    }   
                }
                else
                    return null;
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
                //Fetch All the details with the given noteId and userId
                var notes = fundooContext.NotesTable.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.IsTrash == false)
                    {
                        notes.IsTrash = true;
                        //Save Changes
                        fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsTrash = false;
                        //Save Changes
                        fundooContext.SaveChanges();
                        return notes;
                    }
                }
                else
                    return null;
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
                //Fetch All the details with the given noteId and userId
                var notes = fundooContext.NotesTable.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.IsPin == false)
                    {
                        notes.IsPin = true;
                        //Save Changes
                        fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsPin = false;
                        //Save Changes
                        fundooContext.SaveChanges();
                        return notes;
                    }
                }
                else
                    return null;
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
                //Fetch All the details with the given noteId.
                var notes = fundooContext.NotesTable.Where( n => n.NotesId == noteId ).FirstOrDefault();
                if (notes != null)
                {
                    //insert color
                    notes.Color = color;
                    //Save Changes
                    fundooContext.SaveChanges();
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
        //Method to Upload a Image
        public Notes UploadImage(long noteId, long userId, IFormFile image)
        {
            try
            {
                var note = fundooContext.NotesTable.FirstOrDefault(n => n.NotesId == noteId && n.Id == userId);
                if (note != null)
                {
                    Account acc = new Account(cloudName, apiKey, apiSecret);
                    Cloudinary cloud = new Cloudinary(acc);
                    var imagePath = image.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, imagePath),
                    };
                    var uploadResult = cloud.Upload(uploadParams);
                    note.Image = image.FileName;
                    fundooContext.NotesTable.Update(note);
                    int upload = fundooContext.SaveChanges();
                    if (upload > 0)
                        return note;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }  
        }
        //Make Copy of Notes
        public Notes MakeCopyOfNote(long noteId, long userId)
        {
            try
            {
                var note = fundooContext.NotesTable.FirstOrDefault(n => n.NotesId == noteId && n.Id == userId);
                if (note != null)
                {
                    Notes newNotes = new Notes();
                    newNotes.Title = note.Title;
                    newNotes.Description = note.Description;
                    newNotes.Color = note.Color;
                    newNotes.Image = note.Image;
                    newNotes.IsArchieve = note.IsArchieve;
                    newNotes.IsTrash = note.IsTrash;
                    newNotes.IsPin = note.IsPin;
                    newNotes.CreateAt = note.CreateAt;
                    newNotes.ModifiedAt = note.ModifiedAt;
                    newNotes.Id = userId;
                    //Add newNotes in database
                    fundooContext.NotesTable.Add(newNotes);
                    //Save Changes made in database
                    fundooContext.SaveChanges();
                    return note;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }                   
        }
    }
}
