using Fortnite.GameEngine;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Fortnite.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Random _rnd = new Random();
        private const int StartHealth = 100;
        private const int StartNumberOfWeapons = 1;

        FortnitePlayer fortnitePlayer;
        FortniteTeam fortniteTeam;

        public MainWindow()
        {
            InitializeComponent();
            fortnitePlayer = new FortnitePlayer("Howest Student", StartHealth, StartNumberOfWeapons);
            fortniteTeam = new FortniteTeam();
            lblPlayerName.Content = fortnitePlayer.NamePlayer;
            lblHealth.Content = fortnitePlayer.Health;
            lblWeapons.Content = fortnitePlayer.NumberOfWeapons;
        }

        private void BtnPickUpHealth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int healthPack = _rnd.Next(1, FortnitePlayer.MaximumHealth);
                fortnitePlayer.PickUpHealthPack(healthPack);
                lblHealth.Content = fortnitePlayer.Health;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnPickUpWeapon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fortnitePlayer.PickUpWeapon();
                lblWeapons.Content = fortnitePlayer.NumberOfWeapons;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnGetShot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int damage = _rnd.Next(1, FortnitePlayer.MaximumDamage);
                fortnitePlayer.GetShot(damage);
                lblHealth.Content = fortnitePlayer.Health;
                CheckIfGameIsOver();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckIfGameIsOver()
        {
            if (fortnitePlayer.Health == 0)
            {
                lblHealth.Foreground = Brushes.Red;
                btnGetShot.IsEnabled = false;
                btnPickUpHealth.IsEnabled = false;
                btnPickUpWeapon.IsEnabled = false;
                lblGameOver.Foreground = Brushes.White;
                lblGameOver.Background = Brushes.Red;
                lblGameOver.Content = "Game Over!";
            }
        }

        private void BtnAddPlayerToTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FortnitePlayer player = new FortnitePlayer("Player", StartHealth, StartNumberOfWeapons);
                fortniteTeam.AddPlayerToTeam(player);
                UpdateTeamList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRemovePlayerToTeam_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FortnitePlayer playerToRemove = (FortnitePlayer)lstPlayersInTeam.SelectedItem;
                fortniteTeam.RemovePlayerFromTeam(playerToRemove);
                UpdateTeamList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTeamList()
        {
            lstPlayersInTeam.Items.Clear();
            List<FortnitePlayer> playersInTeam = fortniteTeam.PlayersInTeam;

            foreach (FortnitePlayer player in playersInTeam)
            {
                lstPlayersInTeam.Items.Add(player);
            }
        }
    }
}
