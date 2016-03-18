using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace WisconsinTrackClubWebsite.Models
{
    public class UserForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserFormId { get; set; }
        public bool IsComplete { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        //public Guid FormId { get; set; }
        //[ForeignKey("FormId")]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Form Form { get; set; }
    }
}