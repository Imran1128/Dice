using System;
using System.Collections.Generic;

namespace DiceGame
{
    public class Dice
    {
        public List<int> Sides { get; }
        private static Random rand = new Random();

        public Dice(List<int> sides)
        {
            Sides = sides;
        }

        public int Roll()
        {
            int index = rand.Next(Sides.Count);
            return Sides[index];
        }

        public override string ToString()
        {
            return string.Join(",", Sides);
        }
    }
}
