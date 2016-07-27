using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisconsinTrackClubWebsite.Models
{
    public class UserRunEntry
    {
        public string UserRunEntryId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual RunEntry Run { get; set; }
        
    }
}
