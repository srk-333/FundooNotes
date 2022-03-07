using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class LabelRL : ILabelRL
    {
        //instance of Classes
        private readonly FundooContext fundooContext;
        //Constructor
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        //Add Label Name For any Notes in the database.
        public Labels AddLabelName(string labelName, long noteId ,long  userId)
        {
            try
            {
                Labels label = new Labels();
                label.LabelName = labelName;
                label.Id = userId;
                label.NotesId = noteId;
                //Add all the details in Label Table.
                fundooContext.LabelTable.Add(label);
                //Save Changes Made in the database
                int result = fundooContext.SaveChanges();
                if (result > 0)
                    return label;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to Update the existing Label Name in the database.
        public Labels UpdateLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                //Fetch All the details with the given noteId and userId.
                var label = fundooContext.LabelTable.FirstOrDefault(l => l.NotesId == noteId && l.Id == userId);
                if (label != null)
                {
                    label.LabelName = labelName;
                    //Update database for given LabelName.
                    fundooContext.LabelTable.Update(label);
                    //Save Changes Made in the database
                    fundooContext.SaveChanges();
                    return label;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to Delete a Label from Database
        public bool RemoveLabel(long labelId , long userId)
        {
            try
            {
                //Fetch All the details with the given labelId.
                var labelDetails = fundooContext.LabelTable.FirstOrDefault(l => l.LabelId == labelId && l.Id == userId);
                if (labelDetails != null)
                {
                    //Remove Label details from database
                    fundooContext.LabelTable.Remove(labelDetails);
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
        //Get All Labels Details by NoteId 
        public IEnumerable<Labels> GetByNoteId(long noteId)
        {
            try
            {
                var data = fundooContext.LabelTable.Where(d => d.NotesId == noteId).ToList();
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
        //Get All Labels Details by UserId 
        public IEnumerable<Labels> GetByUserId(long userId)
        {
            try
            {
                var data = fundooContext.LabelTable.Where(d => d.Id == userId).ToList();
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
    }
}
