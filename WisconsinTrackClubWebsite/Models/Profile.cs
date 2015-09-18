using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WisconsinTrackClubWebsite.Models
{
    public class Profile
    {
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Year In School")]
        public string YearInSchool { get; set; } //Should convert to Lookup Table like RunningEmphasis
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddressAttribute]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Profile Visibility")]
        public bool? IsVisible { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        [DisplayFormat(DataFormatString = "{0:###-###-####}")]
        public String PhoneNumber { get; set; }
        public string Sex { get; set; }
        public string Major { get; set; }    //Could also be converted to loolup table for all the Majors offered at the school, but maintenance could be an issue.
        public bool Approved { get; set; }
        public String FacebookProviderKey { get; set; }
        public string State { get; set; }

        public DateTime Birthday { get; set; }
        public RunningEmphasis RunningEmphasis { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        [Display(Name = "Emergency Contact Name")]
        public String EmergencyContactName { get; set; }
        [Display(Name = "Phone Number")]
        [DisplayFormat(DataFormatString = "{0:###-###-####}")]
        public String EmergencyContactPhoneNumber { get; set; }
        [Display(Name = "Background Color")]
        public String BackgroundColor { get; set; }

    }

   
}