using System;
using System.Collections.Generic;
using System.Security.Cryptography;  
using System.Text;                 

namespace DiceGame
{
    public class Game
    {
        public static void PlayGame(List<Dice> userDice, List<Dice> computerDice)
        {
            Console.WriteLine("Let's determine who makes the first move.");
            byte[] keyForFirstMove = FairRandomNumberGenerator.GenerateKey();
            int computerFirstMove = FairRandomNumberGenerator.GenerateFairRandomNumber(0, 1);
            Console.WriteLine($"I selected a random value in the range 0..1 (HMAC={GenerateHMAC(keyForFirstMove, computerFirstMove)})");
            Console.WriteLine("Try to guess my selection.");
            Console.WriteLine("0 - 0");
            Console.WriteLine("1 - 1");
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");

            string userFirstChoice = Console.ReadLine();
            if (userFirstChoice == "X")
            {
                Environment.Exit(0);
            }

            if (userFirstChoice == "?")
            {
                HelpTable.DisplayHelpTable(userDice, computerDice);
                return;
            }

            int userFirstSelection = int.Parse(userFirstChoice);
            Console.WriteLine($"Your selection: {userFirstSelection}");
            Console.WriteLine($"My selection: {computerFirstMove} (KEY={BitConverter.ToString(keyForFirstMove).Replace("-", "")})");

            bool isComputerFirst = computerFirstMove == 1;
            if (isComputerFirst)
            {
                Console.WriteLine("I make the first move.");
            }
            else
            {
                Console.WriteLine("You make the first move.");
            }

            if (isComputerFirst)
            {
                PlayRound(computerDice, userDice);
            }
            else
            {
                PlayRound(userDice, computerDice);
            }
        }

        private static void PlayRound(List<Dice> activeDice, List<Dice> opponentDice)
        {
            var selectedDice = SelectDice(activeDice);
            int computerNumber = FairRandomNumberGenerator.GenerateFairRandomNumber(0, selectedDice.Sides.Count - 1);
            Console.WriteLine($"I selected a random value in the range 0..{selectedDice.Sides.Count - 1} (HMAC={GenerateHMAC(BitConverter.GetBytes(computerNumber), computerNumber)})");

            int userNumber = GetUserNumber(selectedDice.Sides.Count);
            int opponentNumber = FairRandomNumberGenerator.GenerateFairRandomNumber(0, selectedDice.Sides.Count - 1);

            
            if (opponentNumber < 0 || opponentNumber >= selectedDice.Sides.Count)
            {
                Console.WriteLine("Error: Generated opponent number is out of range.");
                return;
            }

            Console.WriteLine($"My number is {opponentNumber} (KEY={BitConverter.ToString(BitConverter.GetBytes(opponentNumber)).Replace("-", "")})");
            Console.WriteLine($"The result is {userNumber} + {opponentNumber} = {(userNumber + opponentNumber) % selectedDice.Sides.Count} (mod {selectedDice.Sides.Count})");

            int userRoll = selectedDice.Sides[userNumber];
            int opponentRoll = selectedDice.Sides[opponentNumber];
            Console.WriteLine($"Your roll is {userRoll}, my roll is {opponentRoll}");

            if (userRoll > opponentRoll)
                Console.WriteLine("You win!");
            else if (userRoll < opponentRoll)
                Console.WriteLine("I win!");
            else
                Console.WriteLine("It's a tie!");
        }



        private static Dice SelectDice(List<Dice> diceList)
        {
            Console.WriteLine("Choose your dice:");
            for (int i = 0; i < diceList.Count; i++)
            {
                Console.WriteLine($"{i}: {string.Join(",", diceList[i].Sides)}");
            }
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");
            string choice = Console.ReadLine();
            if (choice == "X")
                Environment.Exit(0);
            if (choice == "?")
                HelpTable.DisplayHelpTable(diceList, diceList);
            return diceList[int.Parse(choice)];
        }

        private static int GetUserNumber(int max)
        {
            Console.WriteLine($"Add your number modulo {max}");
            Console.WriteLine("0 - 0");
            Console.WriteLine("1 - 1");
            Console.WriteLine("2 - 2");
            Console.WriteLine("3 - 3");
            Console.WriteLine("4 - 4");
            Console.WriteLine("5 - 5");
            Console.WriteLine("X - exit");
            string choice = Console.ReadLine();
            if (choice == "X")
                Environment.Exit(0);
            return int.Parse(choice);
        }

        private static string GenerateHMAC(byte[] key, int value)
        {
            using (var hmac = new HMACSHA256(key))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value.ToString()));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}
