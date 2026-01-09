using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GameProgII_Classes_Tweedale
{
    class Program
    {
        public static int health = 100;
        public static int shield = 50;
        public static Random rand;
        public static Player player;

        static void Main()
        {
            Console.CursorVisible = false;

            rand = new Random();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter a name for your hero: ");
            Console.ForegroundColor = ConsoleColor.White;

            string inputName = Console.ReadLine();
            Console.Clear();

            player = new Player(name: inputName, maxHealth: health, maxShield: shield);

            while (true) 
            {
                DrawHUD();

                Console.WriteLine();
                Console.WriteLine("Press D to take damage or H to heal.");

                int value = rand.Next(1, 21);

                bool validInput = false;

                while (!validInput) 
                {
                    char input = Console.ReadKey(true).KeyChar;

                    if (input == 'D' || input == 'd') 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"You took {value} damage!");
                        Console.ForegroundColor = ConsoleColor.White;

                        player.TakeDamage(value);
                        validInput = true;
                    }
                    else if (input == 'H' || input == 'h')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You regained {value} health!");
                        Console.ForegroundColor = ConsoleColor.White;

                        player.Health.Heal(value);
                        validInput = true;
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Press any key to continue...");

                DrawHUD();

                Console.ReadKey(true);
                Console.Clear();

                if (player.Health.CurrentHealth <= 0) break;
            }

            DrawHUD();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You died!");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press any key...");
            Console.ReadKey(true);
        }

        public static void DrawHUD() 
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"                                                                                            ");
            Console.SetCursorPosition(0, 0);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{player.Name} | Health: {player.Health.CurrentHealth}/{player.Health.MaxHealth} | Shield: {player.Shield.CurrentHealth}/{player.Shield.MaxHealth} | Status: {player.GetStatus()}");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }

    public class Health
    {
        public int CurrentHealth { get; private set; }

        public int MaxHealth { get; private set; }

        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public int TakeDamage(int damage)
        {
            if (damage >= 0)
            {
                int spillover = Math.Max(0, damage - CurrentHealth);
                CurrentHealth = Math.Max(0, CurrentHealth - damage);
                return spillover;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Damage cannot be less than 0!");
                Console.ForegroundColor = ConsoleColor.White;
                return 0;
            }

        }

        public void Restore()
        {
            CurrentHealth = MaxHealth;
        }

        public void Heal(int healing)
        {
            if (healing >= 0)
            {
                CurrentHealth = Math.Min(MaxHealth, CurrentHealth + healing);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Healing cannot be less than 0!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

    public class Player 
    {
        public string Name { get; private set; }
        public Health Health { get; private set; }
        public Health Shield { get; private set; }

        public Player(string name, int maxHealth, int maxShield) 
        {
            Name = name;
            Health = new Health(maxHealth);
            Shield = new Health(maxShield);
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