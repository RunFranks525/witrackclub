using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace WisconsinTrackClubWebsite.Models
{
    [Table("UserRaces", Schema = "WiTrackClub")]
    public class UserRace
    {
        public Guid Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Race Race { get; set; }
        public string HighLights { get; set; }
        public int Count { get; set; }
    }
}