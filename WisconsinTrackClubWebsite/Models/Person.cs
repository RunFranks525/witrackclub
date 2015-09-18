using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WisconsinTrackClubWebsite.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Bio { get; set; }
        public string ImagePath { get; set; }
        public string Grade { get; set; }
    }
}