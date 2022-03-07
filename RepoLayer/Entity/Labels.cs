using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepoLayer.Entity
{
    public class Labels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }       
        [ForeignKey("note")]
        public long NotesId { get; set; }
        public Notes notes { get; set; }
        [ForeignKey("user")]
        public long Id { get; set; }
        public User user { get; set; }
    }
}
