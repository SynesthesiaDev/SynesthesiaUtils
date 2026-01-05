using System;

namespace SynesthesiaUtil.Extensions;

public static class DoubleExtensions
{
    public static float ToFloat(this double source)
    {
        return (float)source;
    }

    public static double FindPrecision(this double source)
    {
        var precision = 0;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        while (source != Math.Round(source))
        {
            source *= 10;
            precision++;
        }

        return precision;
    }

    public static double FloorToDecimalDigits(this double value, uint digits)
    {
        var base10 = Math.Pow(10, digits);
        return Math.Floor(value * base10) / base10;
    }

    public static int RoundBPM(this double baseBpm, double rate = 1) => (int)Math.Round(baseBpm * rate);
}