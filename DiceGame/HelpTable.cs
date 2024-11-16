using System;
using System.Collections.Generic;

namespace DiceGame
{
    public class HelpTable
    {
        public static void DisplayHelpTable(List<Dice> userDice, List<Dice> computerDice)
        {
            Console.WriteLine("Help: Probability of Winning for each dice pair");
            ProbabilityCalculator.DisplayWinningProbabilities(userDice, computerDice);
        }
    }
}
