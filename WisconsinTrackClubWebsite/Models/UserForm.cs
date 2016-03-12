namespace WisconsinTrackClubWebsite.Models
{
    public class UserForm
    {
        public string UserFormId { get; set; }
        public bool IsComplete { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Form Form { get; set; }

    }
}