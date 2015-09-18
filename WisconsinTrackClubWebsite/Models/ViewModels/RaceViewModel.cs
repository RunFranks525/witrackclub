using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class RaceViewModel
    {
        public Profile Profile { get; set; }
        public List<Race> Races { get; set; }
        public int RaceId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string HighLights { get; set; }


    }
}