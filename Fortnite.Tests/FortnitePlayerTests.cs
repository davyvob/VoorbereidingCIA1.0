using System;
using System.Collections.Generic;
using System.Text;
using Fortnite.GameEngine;
using Xunit;

namespace Fortnite.Tests
{
    public class FortnitePlayerTests
    {
        //tests on GetShot()

        [Fact]
        public void GetSHot_ValidDamageAmount_returnActualHealth()
        {
            //Arrange
            FortnitePlayer player = new FortnitePlayer("testPlayer", FortnitePlayer.MaximumHealth, 1);
            int damage = 20;
            int expectedRemainingHealth = 80;

            //Act
            player.GetShot(damage);
            int ActualHealth = player.Health;

            //Assert
            Assert.Equal(expectedRemainingHealth, ActualHealth);
        }

        [Fact]
        public void GetShot_DamageBiggerThanMaxDamage_ReturnHealthMinusMaxDamage()
        {
            //Arrange
            FortnitePlayer player = new FortnitePlayer("testPlayer", FortnitePlayer.MaximumHealth, 1);
            int damage = 50;
            int expectedRemainingHealth = 70;

            //Act
            player.GetShot(damage);
            int ActualHealth = player.Health;


            //Assert
            Assert.Equal(expectedRemainingHealth, ActualHealth);
        }

        [Fact]
        public void GetShot_DamageBiggerThanMaxHealth_ReturnZeroHealth()
        {
            //Arrange
            FortnitePlayer player = new FortnitePlayer("testPlayer", 20, 1);
            int damage = 25;
            int expectedRemainingHealth = 0;

            //Act
            player.GetShot(damage);
            int ActualHealth = player.Health;


            //Assert
            Assert.Equal(expectedRemainingHealth, ActualHealth);
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(-10, 100)]
        public void GetShot_WithNegativeOrZeroDamage_ThrowArgumentOutOfRangeException(int damage, int initialPlayerHealth)
        {
            //Arrange
            FortnitePlayer player = new FortnitePlayer("testPlayer", initialPlayerHealth, 1);
            string expectedErrorMessage = FortnitePlayer.DamageCannotBeZeroOrLessThanZeroMessage;

            //Act on assert
            var actualException = Assert.Throws<ArgumentOutOfRangeException>(() => player.GetShot(damage));
            Assert.Equal(expectedErrorMessage, actualException.Message);
        }


        //Tests on PickUpWeapon

        [Fact]
        public void PickUpWeapon_WhileHoldingValidAmountOfWeapons_ReturnsActualAmountOfWeapons()
        {
            //Arrange
            int weaponsAllreadyInPossesion = 2;
            FortnitePlayer player = new FortnitePlayer("testPlayer", FortnitePlayer.MaximumHealth, weaponsAllreadyInPossesion);
            int ExpectedAmountOfWeapons = 3;


            //Act
            player.PickUpWeapon();
            int ActualAmountOfWeapons = player.NumberOfWeapons;

            //Assert
            Assert.Equal(ExpectedAmountOfWeapons, ActualAmountOfWeapons);

        }

        [Fact]
        public void PickUpWeapon_WhileHoldingMaxAmountOfWeapons_ThrowArgumentOutOfRangeException()
        {
            //Arrange
            int weaponsAllreadyInPossesion = 5;
            FortnitePlayer player = new FortnitePlayer("testPlayer", FortnitePlayer.MaximumHealth, weaponsAllreadyInPossesion);
            string expectedErrorMessage = FortnitePlayer.MaximumWeaponsReachedMessage;

            //Act on assert
            var actualException = Assert.Throws<ArgumentOutOfRangeException>(() => player.PickUpWeapon());
            Assert.Equal(expectedErrorMessage, actualException.Message);
        }


        //Tests on PickUpHealthPack

        [Fact]
        public void PickUpHealthPack_WithValidAmount_ReturnsActualHealth()
        {
            //Assert
            int playerHealth = 20;
            FortnitePlayer player = new FortnitePlayer("testPlayer", playerHealth, 2);
            int healthPackValue = 40;
            int expectedHealedPlayerHealth = 60;

            //Act
            player.PickUpHealthPack(healthPackValue);
            int ActualHealedPlayerHealth = player.Health;

            //Assert
            Assert.Equal(expectedHealedPlayerHealth, ActualHealedPlayerHealth);
        }

        [Fact]
        public void PickUpHealthPack_WithSumOfhHealthPackAndHealthExceedingMaxHealth_ReturnsActualHealth()
        {
            //Assert
            int initialPlayerHealth = 20;
            FortnitePlayer player = new FortnitePlayer("testPlayer", initialPlayerHealth, 2);
            int healthPackValue = 120;
            int expectedHealedPlayerHealth = 100;

            //Act
            player.PickUpHealthPack(healthPackValue);
            int ActualHealedPlayerHealth = player.Health;

            //Assert
            Assert.Equal(expectedHealedPlayerHealth, ActualHealedPlayerHealth);
        }

        [Theory]
        [InlineData(0, 20)]
        [InlineData(-10, 20)]
        public void PickUpHealthPack_WithNegativeOrZeroHealthPackValue_ThrowsArgumentOutOfRangeException(int healthPackValue, int initialPlayerHealth)
        {
            //Assert

            FortnitePlayer player = new FortnitePlayer("testPlayer", initialPlayerHealth, 2);
            string expectedErrorMessage = FortnitePlayer.HealthPackCannotBeZeroOrLessThanZeroMessage;

            //Act on Assert
            var actualException = Assert.Throws<ArgumentOutOfRangeException>(() => player.PickUpHealthPack(healthPackValue));
            Assert.Equal(expectedErrorMessage, actualException.Message);
        }
    }
}
