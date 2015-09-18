using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models.ViewModels
{
    public class RemoveUserRaceViewModel
    {
        public int UserRaceId { get; set; }
        public Race Race { get; set; }
    }
}