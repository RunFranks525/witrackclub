using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models
{
    public class UserRace
    {
        public int UserRaceId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Race Race { get; set; }
        public string HighLights { get; set; }
        public int Count { get; set; }
    }
}