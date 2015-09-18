using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WisconsinTrackClubWebsite.Models;

namespace WisconsinTrackClubWebsite.Models
{
    public class Position
    {
        public int PositionID { get; set; }
        public int StudentID { get; set; }

        public virtual Officer Officer { get; set; }
    }
}