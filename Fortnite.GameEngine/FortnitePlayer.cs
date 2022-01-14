using System;

namespace Fortnite.GameEngine
{
    public class FortnitePlayer
    {
        public const int MaximumDamage = 30;
        public const int MaximumHealth = 100;
        public const int MaximumNumberOfWeapons = 5;
        public const string DamageCannotBeZeroOrLessThanZeroMessage = "Damage cannot be zero or less than zero";
        public const string HealthPackCannotBeZeroOrLessThanZeroMessage = "Healthpack cannot be zero or less than zero";
        public const string MaximumWeaponsReachedMessage = "You can't carry any more weapons";

        public Guid PlayerId { get; private set; }
        public string NamePlayer { get; private set; }
        public int Health { get; private set; }
        public int NumberOfWeapons { get; private set; }

        public FortnitePlayer(string namePlayer, int health, int weapons)
        {
            PlayerId = new Guid();
            NamePlayer = namePlayer;
            Health = health;
            NumberOfWeapons = weapons;
        }

        public void GetShot(int damage)
        {
            if (damage >= Health)
            {
                Health = 0;
            }

            else if (damage > MaximumDamage)
            {
                if(MaximumDamage > Health)
                {
                    Health -= MaximumDamage;
                }
                Health -= MaximumDamage;
            }
             
            else if (damage <= 0)
            {
                throw new ArgumentOutOfRangeException(null, DamageCannotBeZeroOrLessThanZeroMessage);
            }
       
            else
            {
                Health -= damage;
            }
                          
        }

        public void PickUpWeapon()
        {
            if (NumberOfWeapons < MaximumNumberOfWeapons)
            {
                NumberOfWeapons++;
            }
            else
            {
                throw new ArgumentOutOfRangeException(null, MaximumWeaponsReachedMessage);
            }
        }

        public void PickUpHealthPack(int healthPack)
        {
            if (healthPack <= 0)
            {
                throw new ArgumentOutOfRangeException(null, HealthPackCannotBeZeroOrLessThanZeroMessage);
            }
            if (Health + healthPack > MaximumHealth)
            {
                Health = MaximumHealth;
            }
            else
            {
                Health += healthPack;
            }
        }

        public override string ToString()
        {
            return $"{NamePlayer}\t{Health}";
        }
    }
}
