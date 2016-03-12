using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WisconsinTrackClubWebsite.Models
{
    //[Table("Race", Schema = "WiTrackClub")]
    public class Race
    {
        public string RaceId { get; set; }
        [Required]
        public string RaceName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime Date { get; set; } 
        public DateTime RegistrationDeadline { get; set; }

    }
}