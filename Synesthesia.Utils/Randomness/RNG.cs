// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Synesthesia.Utils.Randomness;

public static class Rng
{
    private static Random random = new();

    public static void SetSeed(int seed)
    {
        random = new Random(seed);
    }

    public static void RandomBytes(byte[] buffer) => random.NextBytes(buffer);

    public static int RandomInt(int min, int max) => random.Next(min, max);

    public static float RandomFloat(float min, float max) => (float)(RandomDouble(min, max));

    public static double RandomDouble(double min, double max) => random.NextDouble() * (max - min) + min;

    public static long RandomLong(long min, long max) => random.NextInt64(min, max);
}
