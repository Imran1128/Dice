using System;
using System.Collections.Generic;

namespace DiceGame
{
    public class ProbabilityCalculator
    {
        public static void DisplayWinningProbabilities(List<Dice> userDice, List<Dice> computerDice)
        {
            Console.WriteLine("Winning probabilities table:");
            Console.WriteLine("User Dice | Computer Dice | Probability of Winning");

            foreach (var userDie in userDice)
            {
                foreach (var computerDie in computerDice)
                {
                    double probability = CalculateProbability(userDie, computerDie);
                    Console.WriteLine($"{string.Join(",", userDie.Sides)} | {string.Join(",", computerDie.Sides)} | {probability:F2}");
                }
            }
        }

        private static double CalculateProbability(Dice userDie, Dice computerDie)
        {
            int userWins = 0;
            int totalRolls = 0;

            foreach (var userSide in userDie.Sides)
            {
                foreach (var computerSide in computerDie.Sides)
                {
                    totalRolls++;
                    if (userSide > computerSide)
                        userWins++;
                }
            }

            return (double)userWins / totalRolls;
        }
    }
}
