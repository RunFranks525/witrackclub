namespace WisconsinTrackClubWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "State", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "State");
        }
    }
}
