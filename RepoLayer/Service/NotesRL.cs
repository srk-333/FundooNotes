//-----------------------------------------------------------------------
// <copyright file="NotesRL.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepoLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using RepoLayer.Context;
    using RepoLayer.Entity;
    using RepoLayer.Interface;

    /// <summary>
    ///  Service Class
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.INotesRL" />
    public class NotesRL : INotesRL
    {
        /// <summary>
        /// cloud name
        /// </summary>
        private const string CloudName = "saurav-kr";

        /// <summary>
        ///  key of Account
        /// </summary>
        private const string ApiKey = "623933474268242";

        /// <summary>
        ///  secret of Account
        /// </summary>
        private const string ApiSecret = "xpFVFqhxwtaTHq-b4hv8v_2eZLQ";

        /// <summary>
        /// instance of Classes
        /// </summary>
        private readonly FundooContext fundooContext;

        /// <summary>Initializes a new instance of the <see cref="NotesRL" /> class.</summary>
        /// <param name="fundooContext">The fundo context.</param>
        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;       
        }

        /// <summary>
        /// Method to Create a Note with some details.
        /// </summary>
        /// <param name="notesModel"> Takes notes model class </param>
        /// <param name="userId"> Takes user id </param>
        /// <returns> Notes Details  </returns>
        public Notes CreateNote(NotesModel notesModel, long userId)
        {
            try
            {
                Notes notes = new Notes
                {
                    Title = notesModel.Title,
                    Description = notesModel.Description,
                    Color = notesModel.Color,
                    Image = notesModel.Image,
                    IsArchieve = notesModel.IsArchieve,
                    IsTrash = notesModel.IsTrash,
                    IsPin = notesModel.IsPin,
                    CreateAt = notesModel.CreateAt,
                    ModifiedAt = notesModel.ModifiedAt,
                    Id = userId
                };

                // Add all the details in Notes Table.
                this.fundooContext.NotesTable.Add(notes);

                // Save Changes Made in the database
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return notes;
                }
                else
                {
                    return null;
                }                   
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Update the existing Notes in the system.
        /// </summary>
        /// <param name="notesModel">Takes Update Note model class</param>
        /// <param name="noteId"> Takes note id </param>
        /// <returns> Updated Notes Details  </returns>
        public Notes UpdateNote(UpdateNote notesModel, long noteId)
        {
            try
            {
                // Fetch All the details with the given noteId.
                var note = this.fundooContext.NotesTable.Where(u => u.NotesId == noteId).FirstOrDefault();
                if (note != null)
                {
                    note.Title = notesModel.Title;
                    note.Description = notesModel.Description;
                    note.Color = notesModel.Color;
                    note.Image = notesModel.Image;
                    note.ModifiedAt = notesModel.ModifiedAt;

                    // Update database for given NoteId.
                    this.fundooContext.NotesTable.Update(note);

                    // Save Changes Made in the database
                    this.fundooContext.SaveChanges();
                    return note;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Delete a Note from Database
        /// </summary>
        /// <param name="noteId"> Takes note id </param>
        /// <returns> True or False </returns> 
        public bool DeleteNote(long noteId)
        {
            try
            {
                // Fetch All the details with the given noteId.
                var notes = this.fundooContext.NotesTable.Where(n => n.NotesId == noteId).FirstOrDefault();
                if (notes != null)
                {
                    // Remove Note details from database
                    this.fundooContext.NotesTable.Remove(notes);

                    // Save Changes Made in the database
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Fetch Notes from Database for the given UserId.
        /// </summary>
        /// <param name="userId"> Takes user id </param>
        /// <returns> Notes Details  </returns>     
        public IEnumerable<Notes> GetNote(long userId)
        {
            try
            {
                // Fetch All the details from Notes Table for the given UserId.
                var notes = this.fundooContext.NotesTable.Where(n => n.Id == userId).ToList();
                if (notes != null)
                {
                    return notes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Fetch All Notes from Database
        /// </summary>
        /// <returns> All Notes Details </returns>        
        public IEnumerable<Notes> GetAllNotes()
        {
            try
            {
                // Fetch All the details from Notes Table
                var notes = this.fundooContext.NotesTable.ToList();
                if (notes != null)
                {
                    return notes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Check is archive or not
        /// </summary>
        /// <param name="noteId"> Takes users id </param>
        /// <param name="userId"> Takes notes id </param>
        /// <returns> Notes Details  </returns>
        public Notes IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var notes = this.fundooContext.NotesTable.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.IsArchieve == false)
                    {
                        notes.IsArchieve = true;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsArchieve = false;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Check IsTrash or Not
        /// </summary>
        /// <param name="noteId"> Takes note id </param>
        /// <param name="userId"> Takes user id </param>
        /// <returns> Notes Details  </returns> 
        public Notes IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var notes = this.fundooContext.NotesTable.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.IsTrash == false)
                    {
                        notes.IsTrash = true;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsTrash = false;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  Method to Check IsPin or Not
        /// </summary>
        /// <param name="noteId"> Takes note id </param>
        /// <param name="userId"> Takes user id </param>
        /// <returns> Notes Details </returns>      
        public Notes IsPinOrNot(long noteId, long userId)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var notes = this.fundooContext.NotesTable.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.IsPin == false)
                    {
                        notes.IsPin = true;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsPin = false;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  Method to implement Color
        /// </summary>
        /// <param name="noteId"> Takes note id </param>
        /// <param name="color"> Takes  name of color </param>
        /// <returns> notes with all fields</returns>
        public Notes DoColour(long noteId, string color)
        {
            try
            {
                // Fetch All the details with the given noteId.
                var notes = this.fundooContext.NotesTable.FirstOrDefault(n => n.NotesId == noteId);
                if (notes != null)
                {
                    // insert color
                    notes.Color = color;
                    this.fundooContext.SaveChanges();
                    return notes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Upload a Image
        /// </summary>
        /// <param name="noteId"> Takes note id </param>
        /// <param name="userId"> Takes user id </param>
        /// <param name="image"> Takes image path </param>
        /// <returns> Notes with all fields </returns>
        public Notes UploadImage(long noteId, long userId, IFormFile image)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var note = this.fundooContext.NotesTable.FirstOrDefault(n => n.NotesId == noteId && n.Id == userId);
                if (note != null)
                {
                    Account acc = new Account(CloudName, ApiKey, ApiSecret);
                    Cloudinary cloud = new Cloudinary(acc);
                    var imagePath = image.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, imagePath),
                    };
                    var uploadResult = cloud.Upload(uploadParams);
                    note.Image = image.FileName;
                    this.fundooContext.NotesTable.Update(note);
                    int upload = this.fundooContext.SaveChanges();
                    if (upload > 0)
                    {
                        return note;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }  
        }

        /// <summary>
        ///  Make Copy of Notes
        /// </summary>
        /// <param name="noteId">Takes note id </param>
        /// <param name="userId"> Takes user id </param>
        /// <returns> copy of note </returns>
        public Notes MakeCopyOfNote(long noteId, long userId) 
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var note = this.fundooContext.NotesTable.FirstOrDefault(n => n.NotesId == noteId && n.Id == userId);
                if (note != null)
                {
                    Notes newNotes = new Notes
                    {
                        Title = note.Title,
                        Description = note.Description,
                        Color = note.Color,
                        Image = note.Image,
                        IsArchieve = note.IsArchieve,
                        IsTrash = note.IsTrash,
                        IsPin = note.IsPin,
                        CreateAt = note.CreateAt,
                        ModifiedAt = note.ModifiedAt,
                        Id = userId
                    };

                    // Add newNotes in database
                    this.fundooContext.NotesTable.Add(newNotes);

                    // Save Changes made in database
                    this.fundooContext.SaveChanges();
                    return note;
                }
                else
                {
                    return null;
                }                 
            }
            catch (Exception)
            {
                throw;
            }                   
        }
    }
}
