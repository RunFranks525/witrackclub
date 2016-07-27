using System;
using System.Collections.Generic;
using WisconsinTrackClubWebsite.Models;


namespace ContosoUniversity.Models
{
    public class Officer
    {
        public int ID { get; set; }
        public Profile Profile { get; set; }
        public String Position { get; set; }
        public String Bio { get; set; }
        public String ImageUrlPath { get; set; }

    }
}