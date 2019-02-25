using System.Data.Entity.Migrations;

namespace BlackJack.DataAccess.Migrations
{
    public partial class ChangeDatatoDateinGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.GameResults", "PlayerRole");
            DropColumn("dbo.Games", "Data");
        }

        public override void Down()
        {
            AddColumn("dbo.Games", "Data", c => c.DateTime(nullable: false));
            AddColumn("dbo.GameResults", "PlayerRole", c => c.Int(nullable: false));
            DropColumn("dbo.Games", "Date");
        }
    }
}
