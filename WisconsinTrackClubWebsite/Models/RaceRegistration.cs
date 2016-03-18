using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WisconsinTrackClubWebsite.Models
{
    public class RaceRegistration
    {
        public Guid RaceRegistrationId { get; set; }
        public virtual Race RegisteredRace { get; set; }
        public Profile RegisteredPerson { get; set; }
        public bool Registered { get; set; }
        [Required]
        public bool CanDrive { get; set; }
        [Display(Name = "Car Capacity")]
        public int CarCapacity { get; set; }
        //[DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
        public DateTime Departure { get; set; }

    }
}