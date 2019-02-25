using BlackJack.BusinessLogic.ViewModels;
using BlackJack.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.BusinessLogic.Helpers
{
    public static class CalculatePointsHelper
    {
        public static List<PlayersViewModel> CalculatePlayersPoints(List<Move> moves, List<Player> players)
        {
            List<PlayersViewModel> playersViewModels = new List<PlayersViewModel>();

            playersViewModels = players.Select(p => new PlayersViewModel
            {
                Id = p.Id,
                Name = p.NickName,
                PlayerRole = p.PlayerRole
            }).ToList();

            foreach (var move in moves)
            {
                var player = playersViewModels.SingleOrDefault(p => p.Id == move.PlayerId);
                player.Points += move.CardPoints;
            }

            return playersViewModels;
        }
    }
}
