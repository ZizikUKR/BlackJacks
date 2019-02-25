using System.Data.Entity.Migrations;

namespace BlackJack.DataAccess.Migrations
{
    public partial class CreateDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameResults",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    GameId = c.Guid(nullable: false),
                    PlayerId = c.Guid(nullable: false),
                    GameStatus = c.Int(nullable: false),
                    PlayerRole = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Games",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    IsFinished = c.Boolean(nullable: false),
                    Data = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Moves",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    MoveNumber = c.Long(nullable: false),
                    PlayerId = c.Guid(nullable: false),
                    CardId = c.Long(nullable: false),
                    CardName = c.String(),
                    CardPoints = c.Byte(nullable: false),
                    GameId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Players",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    NickName = c.String(),
                    PlayerRole = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Players");
            DropTable("dbo.Moves");
            DropTable("dbo.Games");
            DropTable("dbo.GameResults");
        }
    }
}
