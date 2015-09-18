using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class OfficerViewModel
    {
        public List<Person> people { get; set; }
        public Person OfficerInContext { get; set; }



    }
}