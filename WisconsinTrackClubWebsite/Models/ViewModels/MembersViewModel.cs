using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class MembersViewModel
    {
        public Profile UserProfile { get; set; }
        public List<Profile> people { get; set; }
    }
}