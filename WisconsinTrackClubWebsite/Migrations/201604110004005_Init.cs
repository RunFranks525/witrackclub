namespace WisconsinTrackClubWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("WiTrackClub.Races", "Season", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("WiTrackClub.Races", "Season");
        }
    }
}
