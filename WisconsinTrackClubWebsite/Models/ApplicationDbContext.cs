using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace WisconsinTrackClubWebsite.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("LocalConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<RunningEmphasis> RunningEmphases { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Person> Officers { get; set; }
        public DbSet<RaceRegistration> RaceRegistration { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<Race> Races { get; set; }
        //public DbSet<Post> Posts { get; set; }
        public DbSet<UserForm> UserForms { get; set; }
        public DbSet<UserRace> UserRaces { get; set; }
        //public DbSet<UserRunEntry> UserRunEntry { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<WisconsinTrackClubWebsite.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}