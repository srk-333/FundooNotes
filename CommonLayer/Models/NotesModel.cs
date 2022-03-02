using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public  class NotesModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool IsArchieve { get; set; }
        public bool IsTrash { get; set; }
        public bool IsPin { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
