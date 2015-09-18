using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class MyRacesViewModel
    {
        public Profile UserProfile { get; set; }
        public List<RaceViewModel> MyRaces { get; set; }
    }
}