using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ILabelRL
    {
        public Labels AddLabelName(string labelName, long noteId, long userId);
        public Labels UpdateLabelName(string labelName, long noteId, long userId);
        public bool RemoveLabel(long labelId, long userId);
        public IEnumerable<Labels> GetByNoteId(long noteId);
        public IEnumerable<Labels> GetByUserId(long userId);
        public IEnumerable<Labels> GetAllLabels();
    }
}
