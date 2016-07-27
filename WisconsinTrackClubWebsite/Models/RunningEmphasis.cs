using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WisconsinTrackClubWebsite.Models
{
    public class RunningEmphasis
    {
        [Key]
        public string EmphasisId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}