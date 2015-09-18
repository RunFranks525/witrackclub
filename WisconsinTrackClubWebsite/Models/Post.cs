using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace WisconsinTrackClubWebsite.Models
{
    public class Post
    {
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string PostName { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}