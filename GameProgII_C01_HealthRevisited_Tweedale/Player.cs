using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProgII_C01_HealthRevisited_Tweedale
{
    public class Player
    {
        public string Name { get; private set; }
        public Health Health { get; private set; }
        public Health Shield { get; private set; }

        public Player(string name, int maxHealth, int maxShield)
        {
            Name = name;
            Health = new Health(maxHealth: maxHealth);
            Shield = new Health(maxHealth: maxShield);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Damage cannot be less than 0!");
                Console.ForegroundColor = ConsoleColor.White;

                return;
            }

            if (Shield.CurrentHealth > 0)
            {
                int spilloverDamage = Shield.TakeDamage(damage);

                if (spilloverDamage > 0)
                {
                    Health.TakeDamage(spilloverDamage);
                }
            }
            else
            {
                Health.TakeDamage(damage);
            }
        }

        public string GetStatus()
        {
            int health = Health.CurrentHealth;

            if (health <= 0)
            {
                return "Dead";
            }
            else if (health < Health.MaxHealth * 0.25)
            {
                return "Hurting really bad";
            }
            else if (health < Health.MaxHealth * 0.5)
            {
                return "Hurt";
            }
            else if (health < Health.MaxHealth * 0.75)
            {
                return "Aight";
            }
            else
            {
                return "Healthy";
            }
        }
    }
}
