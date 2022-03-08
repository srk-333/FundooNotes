using BusinessLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    //Service Class
    public class CollabBL : ICollabBL
    {
        //instance of RepoLayer Interface
        private readonly ICollabRL collabRL;
        //Constructor
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        //Method to Add Person to Collab.
        public Collabarator AddCollab(string email, long userId, long noteId)
        {
            try
            {
                return collabRL.AddCollab(email, userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }       
        //Method to Remove Collabed Person
        public Collabarator RemoveCollab(long userId, long collabId)
        {
            try
            {
                return collabRL.RemoveCollab( userId, collabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Get Details By CollabId
        public IEnumerable<Collabarator> GetByNoteId(long noteId)
        {
            try
            {
                return collabRL.GetByNoteId(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to Fetch All Collabs data from Database
        public IEnumerable<Collabarator> GetAllCollab()
        {
            try
            {
                return collabRL.GetAllCollab();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
