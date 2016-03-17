namespace WisconsinTrackClubWebsite.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<WisconsinTrackClubWebsite.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WisconsinTrackClubWebsite.Models.ApplicationDbContext context)
        {

        }
    }
}
