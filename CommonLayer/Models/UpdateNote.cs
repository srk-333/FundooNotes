using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class UpdateNote
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
