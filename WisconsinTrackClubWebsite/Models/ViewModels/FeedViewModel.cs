using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class FeedViewModel
    {
        public List<UserRace> UserRaces { get; set; }
        public ApplicationUser Profile { get; set; }
        public Information Post { get; set; }
        public List<Information> Posts { get; set; }
    }
}