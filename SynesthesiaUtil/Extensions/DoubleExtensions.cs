using System;

namespace SynesthesiaUtil.Extensions
{
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
    }
}