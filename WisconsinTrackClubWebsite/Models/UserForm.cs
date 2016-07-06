using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace WisconsinTrackClubWebsite.Models
{
    [Table("UserForms", Schema = "WiTrackClub")]
    public class UserForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public bool IsComplete { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Form Form { get; set; }
    }
}