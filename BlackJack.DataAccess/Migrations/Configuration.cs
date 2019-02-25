using System.Data.Entity.Migrations;

namespace BlackJack.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BlackJack.DataAccess.BlackJackContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlackJack.DataAccess.BlackJackContext context)
        {
        }
    }
}
