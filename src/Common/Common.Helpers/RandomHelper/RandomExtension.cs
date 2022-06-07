namespace Common.Helpers.RandomHelper;

/*
 * Based on posts founded in stackoverflow
 * 
 * - NextInt32: https://stackoverflow.com/a/609529
 * - NextDecimal: https://stackoverflow.com/a/28860710
 */

public static class RandomExtension
{
    /// <summary>
    /// Returns an Int32 with a random value across the entire range of
    /// possible values.
    /// </summary>
    public static int NextInt32(this Random rng)
    {
        int firstBits = rng.Next(0, 1 << 4) << 28;
        int lastBits = rng.Next(0, 1 << 28);
        return firstBits | lastBits;
    }

    //Provides a random decimal value in the range [0.0000000000000000000000000000, 0.9999999999999999999999999999) with (theoretical) uniform and discrete distribution.
    public static decimal NextDecimalSample(this Random random)
    {
        var sample = 1m;
        //After ~200 million tries this never took more than one attempt but it is possible to generate combinations of a, b, and c with the approach below resulting in a sample >= 1.
        while (sample >= 1)
        {
            var a = random.NextInt32();
            var b = random.NextInt32();
            //The high bits of 0.9999999999999999999999999999m are 542101086.
            var c = random.Next(542101087);
            sample = new Decimal(a, b, c, false, 28);
        }
        return sample;
    }

    public static decimal NextDecimal(this Random random)
    {
        return NextDecimal(random, decimal.MaxValue);
    }

    public static decimal NextDecimal(this Random random, decimal maxValue)
    {
        return NextDecimal(random, decimal.Zero, maxValue);
    }

    public static decimal NextDecimal(this Random random, decimal minValue, decimal maxValue)
    {
        var nextDecimalSample = NextDecimalSample(random);
        return maxValue * nextDecimalSample + minValue * (1 - nextDecimalSample);
    }
}

