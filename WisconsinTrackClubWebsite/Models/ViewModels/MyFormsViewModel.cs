using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class MyFormsViewModel
    {
        public Profile UserProfile { get; set; }
        public List<UserFormsViewModel> MyForms { get; set; }
    }
}