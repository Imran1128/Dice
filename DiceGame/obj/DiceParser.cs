using System;
using System.Collections.Generic;
using System.Linq;

namespace DiceGame
{
    public class DiceParser
    {
        public static List<Dice> ParseDice(string[] args)
        {
            var diceList = new List<Dice>();
            foreach (var arg in args)
            {
                try
                {
                    var sides = arg.Split(',').Select(int.Parse).ToList();
                    diceList.Add(new Dice(sides));
                }
                catch (Exception)
                {
                    throw new ArgumentException("Invalid dice configuration: " + arg);
                }
            }
            return diceList;
        }
    }
}
