// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace SynesthesiaUtil.Extensions;

public static class DoubleExtensions
{
    extension(double source)
    {
        public float ToFloat()
        {
            return (float)source;
        }

        public double FindPrecision()
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

        public double FloorToDecimalDigits(uint digits)
        {
            var base10 = Math.Pow(10, digits);
            return Math.Floor(source * base10) / base10;
        }

        public int RoundBpm(double rate = 1) => (int)Math.Round(source * rate);
    }
}
