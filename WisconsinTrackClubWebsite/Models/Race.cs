using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WisconsinTrackClubWebsite.Models
{
    public class Race
    {
        public int RaceId { get; set; }
        [Required]
        public string RaceName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime Date { get; set; } 
        public DateTime RegistrationDeadline { get; set; }

    }
}