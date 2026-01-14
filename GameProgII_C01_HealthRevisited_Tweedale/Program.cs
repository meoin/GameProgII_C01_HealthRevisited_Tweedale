using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GameProgII_C01_HealthRevisited_Tweedale
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

            string inputName = "";

            while (true)
            {
                inputName = Console.ReadLine();

                if (inputName.Any(Char.IsLetter)) break;
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Enter a name for your hero: ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            
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

}