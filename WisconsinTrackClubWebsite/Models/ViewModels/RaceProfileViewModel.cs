using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class RaceProfileViewModel
    {
        public Profile User { get; set; }
        public Information Comment { get; set; }
        public List<Information> Comments { get; set; }
        public Race Race { get; set; }
        public List<Profile> RegisteredPeople { get; set; }
        public List<RaceRegistration> entries { get; set; }
    }
}