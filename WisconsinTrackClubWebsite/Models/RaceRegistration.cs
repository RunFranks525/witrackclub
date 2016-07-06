using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WisconsinTrackClubWebsite.Models
{
    [Table("RaceRegistrations", Schema = "WiTrackClub")]

    public class RaceRegistration
    {
        public Guid Id { get; set; }
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