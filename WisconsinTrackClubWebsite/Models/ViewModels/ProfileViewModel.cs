using System.Collections.Generic;

namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class ProfileViewModel
    {
        public Profile MyProfile { get; set; }
        public List<WisconsinTrackClubWebsite.Models.ViewModels.UserFormsViewModel> MyForms { get; set; }
    }
}