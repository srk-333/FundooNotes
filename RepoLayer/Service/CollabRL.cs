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
    public class CollabRL : ICollabRL
    {
        //instance of Classes
        private readonly FundooContext fundooContext;
        //Constructor
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        //Do Collab with other registerd person
        public Collabarator AddCollab(string email, long userId, long noteId)
        {
            var data = fundooContext.UserTable.FirstOrDefault(d => d.Email == email);
            if (data.Email == email)
            {
                Collabarator collab = new Collabarator();
                collab.CollabEmail = email;
                collab.Id = userId;
                collab.NotesId = noteId;
                fundooContext.CollabTable.Add(collab);
                fundooContext.SaveChanges();
                return collab;
            }
            return null;
        }
        //Remoe Collabed Person
        public Collabarator RemoveCollab(long userId, long collabId)
        {
            var data = fundooContext.CollabTable.FirstOrDefault(d => d.Id == userId && d.CollabId == collabId);
            if (data != null)
            {
                fundooContext.CollabTable.Remove(data);
                fundooContext.SaveChanges();
                return data;
            }
            return null;
        }
        //Get Details By CollabId
        public IEnumerable<Collabarator> GetByNoteId(long noteId)
        {
            try
            {
                var data = fundooContext.CollabTable.Where(c => c.NotesId == noteId).ToList();
                if (data != null)
                    return data;
                else
                    return null;
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
                //Fetch All the details from Collab Table
                var collabs = fundooContext.CollabTable.ToList();
                if (collabs != null)
                {
                    return collabs;
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
