using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace WisconsinTrackClubWebsite.Models
{
    [Table("Information", Schema = "WiTrackClub")]

    public class Information
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Display(Name = "Title")]
        public String UpdateTitle { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Information")]
        [AllowHtml]
        public String Info { get; set; }
        public virtual Profile Author { get; set; }
        public bool Importance { get; set; }
        public String Type { get; set; }
        public int Count { get; set; }
    }
}