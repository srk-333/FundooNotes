using BusinessLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        //instance of RepoLayer Interface
        private readonly ILabelRL labelRL;
        //Constructor
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        //Add Label Name For any Notes
        public Labels AddLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                return labelRL.AddLabelName(labelName , noteId , userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Update the existing Label Name in the database.
        public Labels UpdateLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                return labelRL.UpdateLabelName(labelName , noteId , userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to Delete a Label from Database
        public bool RemoveLabel(long labelId, long userId)
        {
            try
            {
                return labelRL.RemoveLabel(labelId , userId);
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
                return labelRL.GetByNoteId(noteId);
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
                return labelRL.GetByUserId(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
