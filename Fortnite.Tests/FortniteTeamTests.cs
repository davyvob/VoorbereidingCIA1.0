using Fortnite.GameEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Fortnite.Tests
{
    public class FortniteTeamTests
    {
        
        //Tests allready writen in startcode
        [Fact]
        public void AddPlayerToTeam_WithValidPlayer_UpdatesPlayersInTeam()
        {
            // Arrange
            int expected = 1;
            FortnitePlayer fortnitePlayer = new FortnitePlayer("Test Player", 100, 1);
            FortniteTeam fortniteTeam = new FortniteTeam();

            // Act
            fortniteTeam.AddPlayerToTeam(fortnitePlayer);

            // Assert
            int actual = fortniteTeam.PlayersInTeam.Count;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddPlayerToTeam_ExceedsMaximumNumberOfPlayers_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            FortniteTeam fortniteTeam = new FortniteTeam();

            for (int i = 0; i < FortniteTeam.MaximumPlayersInTeam; i++)
            {
                fortniteTeam.AddPlayerToTeam(new FortnitePlayer($"TestPlayer_{i}", 100, 1));
            }

            // Assert on Act
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => fortniteTeam.AddPlayerToTeam(new FortnitePlayer("NewTestPlayer", 100, 1)));
            Assert.Equal(FortniteTeam.MaximumPlayersInTeamMessage, ex.Message);
        }

        [Fact]
        public void RemovePlayerFromTeam_WithValidPlayer_UpdatesPlayersInTeam()
        {
            // Arrange
            int expected = 3;
            FortniteTeam fortniteTeam = new FortniteTeam();

            for (int i = 0; i < FortniteTeam.MaximumPlayersInTeam; i++)
            {
                fortniteTeam.AddPlayerToTeam(new FortnitePlayer($"TestPlayer_{i}", 100, 1));
            }

            FortnitePlayer playerToRemove = fortniteTeam.PlayersInTeam[0];

            // Act
            fortniteTeam.RemovePlayerFromTeam(playerToRemove);

            // Assert
            int actual = fortniteTeam.PlayersInTeam.Count;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemovePlayerFromTeam_WhenThereAreNoPlayersInTeam_ThrowsException()
        {
            // Arrange
            FortniteTeam fortniteTeam = new FortniteTeam();

            // Assert on Act
            Exception ex = Assert.Throws<Exception>(() => fortniteTeam.RemovePlayerFromTeam(new FortnitePlayer("TestPlayerToDelete", 100, 1)));
            Assert.Equal(FortniteTeam.CannotDeletePlayerBecauseNoPlayersInTeamMessage, ex.Message);
        }
    }
}
