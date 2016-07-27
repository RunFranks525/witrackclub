using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class RaceRegistrationViewModel
    {
        public List<Race> Races { get; set; }
        public Profile Profile { get; set; }
        public List<RaceRegistration> RaceRegistrations { get; set; }
        public RaceRegistration RegistrationInFocus { get; set; }
        public List<RaceRegistration> Runners { get; set; }
        public Dictionary<String, List<RaceRegistration>> raceRegistrationsCollection { get; set; }


    }
}