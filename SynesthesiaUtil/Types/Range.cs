using System;
using SynesthesiaUtil.Extensions;

namespace SynesthesiaUtil.Types
{
    public interface Range<T>
    {
        public T Random();
        public T Start { get; }
        public T End { get; }
    };

    public record IntRange(int Start, int End) : Range<int>
    {
        public int Random()
        {
            return new Random().Next(Start, End);
        }
    }

    public record ByteRange(byte Start, byte End) : Range<byte>
    {
        public byte Random()
        {
            return new Random().Next(Start, End).ToByte();
        }
    }

    public record DoubleRange(double Start, double End) : Range<double>
    {
        public double Random()
        {
            return new Random().NextDouble() * (End - Start) + Start;
        }
    }

    public record FloatRange(float Start, float End) : Range<float>
    {
        public float Random()
        {
            return (new Random().NextDouble() * (End - Start) + Start).ToFloat();
        }
    }

    public record LongRange(long Start, long End) : Range<long>
    {
        public long Random()
        {
            return new Random().NextInt64(Start, End);
        }
    }

    public record ShortRange(short Start, short End) : Range<short>
    {
        public short Random()
        {
            return new Random().Next(Start, End).ToShort();
        }
    }
}