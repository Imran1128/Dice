using System;
using System.Collections.Generic;

namespace DiceGame
{
    public class Dice
    {
        public List<int> Sides { get; }

        public Dice(List<int> sides)
        {
            Sides = sides;
        }

        public int Roll()
        {
            Random rand = new Random();
            int index = rand.Next(Sides.Count);
            return Sides[index];
        }
    }
}
