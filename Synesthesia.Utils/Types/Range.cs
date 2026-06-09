// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using Synesthesia.Utils.Extensions;

namespace Synesthesia.Utils.Types
{
    public interface IRange<out T>
    {
        T Random();
        T Start { get; }
        T End { get; }
    };

    public record IntRange(int Start, int End) : IRange<int>
    {
        public int Random()
        {
            return new Random().Next(Start, End);
        }
    }

    public record ByteRange(byte Start, byte End) : IRange<byte>
    {
        public byte Random()
        {
            return new Random().Next(Start, End).ToByte();
        }
    }

    public record DoubleRange(double Start, double End) : IRange<double>
    {
        public double Random()
        {
            return new Random().NextDouble() * (End - Start) + Start;
        }
    }

    public record FloatRange(float Start, float End) : IRange<float>
    {
        public float Random()
        {
            return (new Random().NextDouble() * (End - Start) + Start).ToFloat();
        }
    }

    public record LongRange(long Start, long End) : IRange<long>
    {
        public long Random()
        {
            return new Random().NextInt64(Start, End);
        }
    }

    public record ShortRange(short Start, short End) : IRange<short>
    {
        public short Random()
        {
            return new Random().Next(Start, End).ToShort();
        }
    }
}
