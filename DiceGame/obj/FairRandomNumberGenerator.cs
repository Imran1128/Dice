using System;
using System.Security.Cryptography; 

public static class FairRandomNumberGenerator
{
    private static readonly Random random = new Random();

    public static int GenerateFairRandomNumber(int minValue, int maxValue)
    {
        if (minValue > maxValue)
            throw new ArgumentOutOfRangeException("minValue", "minValue must be less than or equal to maxValue.");

        return random.Next(minValue, maxValue + 1);  
    }

    public static byte[] GenerateKey()
    {
        using (var hmac = new HMACSHA256()) 
        {
            byte[] key = hmac.Key;
            return key;
        }
    }
}
