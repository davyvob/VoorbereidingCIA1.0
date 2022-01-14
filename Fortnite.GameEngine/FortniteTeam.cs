using System;
using System.Collections.Generic;

namespace Fortnite.GameEngine
{
    public class FortniteTeam
    {
        public const int MaximumPlayersInTeam = 4;
        public static readonly string MaximumPlayersInTeamMessage = $"There cannot be more than {MaximumPlayersInTeam} players in the team";
        public const string CannotDeletePlayerBecauseNoPlayersInTeamMessage = "Cannot remove player because there are no players in the team";

        public List<FortnitePlayer> PlayersInTeam { get; set; }

        public FortniteTeam()
        {
            PlayersInTeam = new List<FortnitePlayer>();
        }

        public void AddPlayerToTeam(FortnitePlayer player)
        {
            if (PlayersInTeam.Count <= 3)
            {
                PlayersInTeam.Add(player);
            }
            else
            {
                throw new ArgumentOutOfRangeException(null, MaximumPlayersInTeamMessage);
            }
        }

        public void RemovePlayerFromTeam(FortnitePlayer player)
        {
            if (PlayersInTeam.Count < 1)
            {
                throw new Exception(CannotDeletePlayerBecauseNoPlayersInTeamMessage);
            }
            if (PlayersInTeam.Contains(player))
            {
                PlayersInTeam.Remove(player);
            }
        }
    }
}
