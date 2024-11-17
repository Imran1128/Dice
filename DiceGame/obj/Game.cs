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
            string hmac = GenerateHMAC(keyForFirstMove, computerFirstMove);

            Console.WriteLine($"I selected a random value in the range 0..1 (HMAC={hmac})");

            int userFirstSelection = GetUserFirstSelection(userDice, computerDice);

            Console.WriteLine($"Your selection: {userFirstSelection}");
            Console.WriteLine($"My selection: {computerFirstMove} (KEY={BitConverter.ToString(keyForFirstMove).Replace("-", "")})");

            Dice userSelectedDice;
            Dice computerGeneratedDice;

            if (userFirstSelection == computerFirstMove)
            {
                Console.WriteLine("You make the first move.");
                userSelectedDice = SelectDice(userDice);
                computerGeneratedDice = GenerateRandomDiceList(computerDice, userSelectedDice);
                PlayRound(new List<Dice> { userSelectedDice }, new List<Dice> { computerGeneratedDice });
            }
            else
            {
                Console.WriteLine("I make the first move.");
                computerGeneratedDice = GenerateRandomDiceList(computerDice);
                userSelectedDice = SelectDice(userDice, computerGeneratedDice);
                PlayRound(new List<Dice> { computerGeneratedDice }, new List<Dice> { userSelectedDice });
            }
        }

        private static void PlayRound(List<Dice> activeDice, List<Dice> opponentDice)
        {
            var randomDice = GenerateRandomDiceList(opponentDice);
            var selectedDice = SelectDice(activeDice, randomDice);
            int computerNumber = FairRandomNumberGenerator.GenerateFairRandomNumber(0, selectedDice.Sides.Count - 1);
            Console.WriteLine("It's time for my throw");
            Console.WriteLine($"I selected a random value in the range 0..{selectedDice.Sides.Count - 1} (HMAC={GenerateHMAC(BitConverter.GetBytes(computerNumber), computerNumber)})");

            int userNumber = GetUserNumber(selectedDice.Sides.Count);
            int opponentNumber = FairRandomNumberGenerator.GenerateFairRandomNumber(0, selectedDice.Sides.Count - 1);

            if (opponentNumber < 0 || opponentNumber >= selectedDice.Sides.Count)
            {
                Console.WriteLine("Error: Generated opponent number is out of range.");
                return;
            }

            Console.WriteLine($"My number is {opponentNumber} (KEY={BitConverter.ToString(BitConverter.GetBytes(opponentNumber)).Replace("-", "")})");
            var Computerresult = (userNumber + opponentNumber) % selectedDice.Sides.Count;
            Console.WriteLine($"The result is {userNumber} + {opponentNumber} = {Computerresult} (mod {selectedDice.Sides.Count})");
            int ComputerRoll = randomDice.Sides[Computerresult];

            Console.WriteLine($"My throw is:{ComputerRoll}");
            Console.WriteLine("It's time for Your throw");
            Console.WriteLine($"I selected a random value in the range 0..{selectedDice.Sides.Count - 1} (HMAC={GenerateHMAC(BitConverter.GetBytes(computerNumber), computerNumber)})");

            userNumber = GetUserNumber(selectedDice.Sides.Count);
            opponentNumber = FairRandomNumberGenerator.GenerateFairRandomNumber(0, selectedDice.Sides.Count - 1);

            if (opponentNumber < 0 || opponentNumber >= selectedDice.Sides.Count)
            {
                Console.WriteLine("Error: Generated opponent number is out of range.");
                return;
            }

            Console.WriteLine($"My number is {opponentNumber} (KEY={BitConverter.ToString(BitConverter.GetBytes(opponentNumber)).Replace("-", "")})");
            var YourResult = (userNumber + opponentNumber) % selectedDice.Sides.Count;
            Console.WriteLine($"The result is {userNumber} + {opponentNumber} = {YourResult} (mod {selectedDice.Sides.Count})");
            int UserRoll = selectedDice.Sides[YourResult];

            Console.WriteLine($"Your throw is:{UserRoll}");
            if (UserRoll > ComputerRoll)
                Console.WriteLine($"You win!({UserRoll}>{ComputerRoll})");
            else if (UserRoll < ComputerRoll)
                Console.WriteLine($"I win!({ComputerRoll}>{UserRoll})");
            else
                Console.WriteLine("It's a tie!");
        }

        private static void PlayRound2(List<Dice> activeDice, List<Dice> opponentDice)
        {
            var selectedDice = SelectDice(activeDice);
            var randomDice = GenerateRandomDiceList(opponentDice, selectedDice);
            int computerNumber = FairRandomNumberGenerator.GenerateFairRandomNumber(0, selectedDice.Sides.Count - 1);
            Console.WriteLine("It's time for my throw");
            Console.WriteLine($"I selected a random value in the range 0..{selectedDice.Sides.Count - 1} (HMAC={GenerateHMAC(BitConverter.GetBytes(computerNumber), computerNumber)})");

            int userNumber = GetUserNumber(selectedDice.Sides.Count);
            int opponentNumber = FairRandomNumberGenerator.GenerateFairRandomNumber(0, selectedDice.Sides.Count - 1);

            if (opponentNumber < 0 || opponentNumber >= selectedDice.Sides.Count)
            {
                Console.WriteLine("Error: Generated opponent number is out of range.");
                return;
            }

            Console.WriteLine($"My number is {opponentNumber} (KEY={BitConverter.ToString(BitConverter.GetBytes(opponentNumber)).Replace("-", "")})");
            var Computerresult = (userNumber + opponentNumber) % selectedDice.Sides.Count;
            Console.WriteLine($"The result is {userNumber} + {opponentNumber} = {Computerresult} (mod {selectedDice.Sides.Count})");
            int ComputerRoll = randomDice.Sides[Computerresult];

            Console.WriteLine($"My throw is:{ComputerRoll}");
            Console.WriteLine("It's time for Your throw");
            Console.WriteLine($"I selected a random value in the range 0..{selectedDice.Sides.Count - 1} (HMAC={GenerateHMAC(BitConverter.GetBytes(computerNumber), computerNumber)})");

            userNumber = GetUserNumber(selectedDice.Sides.Count);
            opponentNumber = FairRandomNumberGenerator.GenerateFairRandomNumber(0, selectedDice.Sides.Count - 1);

            if (opponentNumber < 0 || opponentNumber >= selectedDice.Sides.Count)
            {
                Console.WriteLine("Error: Generated opponent number is out of range.");
                return;
            }

            Console.WriteLine($"My number is {opponentNumber} (KEY={BitConverter.ToString(BitConverter.GetBytes(opponentNumber)).Replace("-", "")})");
            var YourResult = (userNumber + opponentNumber) % selectedDice.Sides.Count;
            Console.WriteLine($"The result is {userNumber} + {opponentNumber} = {YourResult} (mod {selectedDice.Sides.Count})");
            int UserRoll = selectedDice.Sides[YourResult];

            Console.WriteLine($"My throw is:{UserRoll}");
            if (UserRoll > ComputerRoll)
                Console.WriteLine($"You win!({UserRoll})>{ComputerRoll})");
            else if (UserRoll < ComputerRoll)
                Console.WriteLine($"I win!({ComputerRoll}>{UserRoll})");
            else
                Console.WriteLine("It's a tie!");
        }

        public static Dice SelectDice(List<Dice> diceList, Dice excludedDice = null)
        {
            Console.WriteLine("Choose your dice:");

            var filteredDiceList = new List<Dice>();

            foreach (var dice in diceList)
            {
                if (dice != excludedDice)
                {
                    filteredDiceList.Add(dice);
                }
            }

            if (filteredDiceList.Count == 0)
            {
                Console.WriteLine("No dice available to choose from.");
                return null;
            }

            for (int i = 0; i < filteredDiceList.Count; i++)
            {
                Console.WriteLine($"{i}: {string.Join(",", filteredDiceList[i].Sides)}");
            }

            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");

            string choice = Console.ReadLine();

            if (choice.ToUpper() == "X")
                Environment.Exit(0);

            if (choice == "?")
            {
                HelpTable.DisplayHelpTable(filteredDiceList, filteredDiceList);
                return SelectDice(diceList, excludedDice);
            }

            if (int.TryParse(choice, out int selectedIndex) && selectedIndex >= 0 && selectedIndex < filteredDiceList.Count)
            {
                Console.WriteLine($"You selected: {string.Join(",", filteredDiceList[selectedIndex].Sides)}");
                return filteredDiceList[selectedIndex];
            }

            Console.WriteLine("Invalid selection, please try again.");
            return SelectDice(diceList, excludedDice);
        }

        public static Dice GenerateRandomDiceList(List<Dice> diceList, Dice excludedDice = null)
        {
            Random random = new Random();
            List<Dice> availableDice = new List<Dice>(diceList);

            if (excludedDice != null)
            {
                availableDice.Remove(excludedDice);
            }

            if (availableDice.Count == 0)
            {
                Console.WriteLine("No dice available to select randomly.");
                return null;
            }

