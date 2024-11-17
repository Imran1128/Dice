using System;
using System.Collections.Generic;

namespace DiceGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Error: You must specify at least three dice configurations.");
                Console.WriteLine("Usage example: game.exe 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
                return;
            }

            try
            {
                List<Dice> userDice = DiceParser.ParseDice(args);
                List<Dice> computerDice = DiceParser.ParseDice(args);

                Game.PlayGame(userDice, computerDice);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Usage example: game.exe 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
            }
        }
    }
}
