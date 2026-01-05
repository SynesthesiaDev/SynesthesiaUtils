using System;

namespace SynesthesiaUtil.Randomness;

public static class RNG
{
    private static Random _random = new();

    public static void SetSeed(int seed)
    {
        _random = new Random(seed);
    }

    public static void RandomBytes(byte[] buffer) => _random.NextBytes(buffer);

    public static int RandomInt(int min, int max) => _random.Next(min, max);

    public static float RandomFloat(float min, float max) => (float)(RandomDouble(min, max));

    public static double RandomDouble(double min, double max) => _random.NextDouble() * (max - min) + min;

    public static long RandomLong(long min, long max) => _random.NextInt64(min, max);
}