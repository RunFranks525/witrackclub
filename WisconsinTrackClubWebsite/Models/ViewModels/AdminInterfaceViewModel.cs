using System;
using System.Collections.Generic;


namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class AdminInterfaceViewModel
    {
        public string ErrorMessage { get; set; }
        public Information Information { get; set; }
        public Dictionary<string, List<UserForm>> UserFormCollection { get; set; }
        public List<Profile> People { get; set; }
        public Profile Current { get; set; }
        public List<RaceRegistration> RaceRegistrations { get; set; }
        public List<Form> Forms { get; set; }
        public List<WisconsinTrackClubWebsite.Models.ViewModels.UserFormsViewModel> UserForms { get; set; }
        List<KeyValuePair<int, Race>> Races { get; set; }

    }
}