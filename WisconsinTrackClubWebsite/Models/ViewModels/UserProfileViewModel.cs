using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public Profile User { get; set; }
        public List<Information> Posts { get; set; }
        public List<UserRace> Races { get; set; }
    }
}