using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WisconsinTrackClubWebsite.Models
{
    public class RunEntry
    {
        public string ID { get; set; }
        [Required]
        public String Course { get; set; }
        [Required]
        public decimal Mileage { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan Pace { get; set; }
    }
}
